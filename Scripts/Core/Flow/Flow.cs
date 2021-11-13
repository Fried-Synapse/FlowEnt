using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides functionality to create a sequence or multiple sequences of animations.
    /// For more information please go to https://flowent.friedsynapse.com/flow
    /// </summary>
    public sealed partial class Flow : AbstractAnimation,
        IUpdateController,
        IFluentFlowEventable<Flow>,
        IFluentFlowOptionable<Flow>
    {
        private const string ErrorAnimationAlreadyStarted = "Cannot add animation that has already started.";

        /// <summary>
        /// Creates a new flow using the options provided.
        /// </summary>
        /// <param name="options"></param>
        public Flow(FlowOptions options)
        {
            CopyOptions(options);
        }

        /// <summary>
        /// Creates a new flow.
        /// </summary>
        /// <param name="autoStart">Whether the flow should start automatically or not.</param>
        public Flow(bool autoStart = false)
        {
            AutoStart = autoStart;
        }

        #region Internal Members

        private readonly FastList<AbstractUpdatable, UpdatableAnchor> updatables = new FastList<AbstractUpdatable, UpdatableAnchor>();
        private readonly List<UpdatableWrapper> updatableWrappersQueue = new List<UpdatableWrapper>(2);
        private UpdatableWrapper lastQueuedUpdatableWrapper;
        private UpdatableWrapper[] updatableWrappersOrderedByTimeIndexed;
        private int nextTimeIndexedUpdatableWrapperIndex;
        private UpdatableWrapper nextTimeIndexedUpdatableWrapper;
        private readonly Dictionary<ulong, UpdatableWrapper> runningUpdatableWrappers = new Dictionary<ulong, UpdatableWrapper>(2);
        private int runningUpdatableWrappersCount;

        private float time;
        private int? remainingLoops;

        #endregion

        public Flow SetName(string name)
        {
            Name = name;
            return this;
        }

        #region IUpdateController

        void IUpdateController.SubscribeToUpdate(AbstractUpdatable updatable)
        {
            updatables.Add(updatable);
        }

        void IUpdateController.UnsubscribeFromUpdate(AbstractUpdatable updatable)
        {
            updatables.Remove(updatable);
        }

        #endregion

        #region Lifecycle

        /// <summary>
        /// Starts the flow.
        /// </summary>
        /// <exception cref="FlowEntException">If the flow has already started.</exception>
        public Flow Start()
        {
            PreStart();
            StartInternal();
            return this;
        }

        /// <summary>
        /// Starts the flow async(you can await this till the flow finishes).
        /// </summary>
        /// <exception cref="FlowEntException">If the flow has already started.</exception>
        public async Task<Flow> StartAsync()
        {
            PreStart();
            StartInternal();
            await new AwaitableAnimation(this);
            return this;
        }

        private void PreStart()
        {
            if (playState != PlayState.Building)
            {
                throw new FlowException(this, "Flow already started.");
            }

            if (autoStartHelper != null)
            {
                CancelAutoStart();
            }
        }

        /// <summary>
        /// Resets the flow so in can be replayed.
        /// </summary>
        /// <exception cref="FlowException">If the flow is not finished.</exception>
        public Flow Reset()
        {
            if (playState != PlayState.Finished)
            {
                throw new FlowException(this, "Can only reset a finished flow. Use Stop() to ensure flow finished when resetting.");
            }

            autoStartHelper = null;
            playState = PlayState.Building;
            overdraft = null;
            time = 0;
            remainingLoops = 0;
            runningUpdatableWrappersCount = 0;
            runningUpdatableWrappers.Clear();
            updatables.Clear();
            return this;
        }

        internal override void StartInternal(float deltaTime = 0)
        {
            if (lastQueuedUpdatableWrapper == null)
            {
                throw new FlowException(this, "Cannot start empty flow.");
            }

            playState = PlayState.Waiting;

            if (skipFrames > 0)
            {
                StartSkipFrames();
                return;
            }

            if (delay > 0f)
            {
                StartDelay();
                return;
            }

            remainingLoops = loopCount;

            Init();

            updateController.SubscribeToUpdate(this);
            playState = PlayState.Playing;
            onStarted?.Invoke();

            UpdateInternal(deltaTime);
        }

        private void Init()
        {
            time = 0;

            if (updatableWrappersOrderedByTimeIndexed == null)
            {
                updatableWrappersOrderedByTimeIndexed = updatableWrappersQueue.ToArray();
                QuickSortByTimeIndex(updatableWrappersOrderedByTimeIndexed, 0, updatableWrappersOrderedByTimeIndexed.Length - 1);
            }

            nextTimeIndexedUpdatableWrapperIndex = 0;
            nextTimeIndexedUpdatableWrapper = updatableWrappersOrderedByTimeIndexed[nextTimeIndexedUpdatableWrapperIndex++];
        }

        internal override void UpdateInternal(float deltaTime)
        {
            float scaledDeltaTime = deltaTime * timeScale;
            time += scaledDeltaTime;

            #region Updating animations

            AbstractUpdatable index = updatables.anchor.next;

            while (index != null)
            {
                index.UpdateInternal(scaledDeltaTime);
                index = index.next;
            }

            #endregion

            #region TimeBased start

            while (nextTimeIndexedUpdatableWrapper != null && time >= nextTimeIndexedUpdatableWrapper.timeIndex)
            {
                ++runningUpdatableWrappersCount;
                AbstractUpdatable updatable = nextTimeIndexedUpdatableWrapper.GetUpdatable();
                runningUpdatableWrappers.Add(updatable.Id, nextTimeIndexedUpdatableWrapper);
                updatable.StartInternal(time - nextTimeIndexedUpdatableWrapper.timeIndex.Value);
                if (nextTimeIndexedUpdatableWrapperIndex < updatableWrappersOrderedByTimeIndexed.Length)
                {
                    nextTimeIndexedUpdatableWrapper = updatableWrappersOrderedByTimeIndexed[nextTimeIndexedUpdatableWrapperIndex++];
                }
                else
                {
                    nextTimeIndexedUpdatableWrapper = null;
                }
            }

            #endregion

            onUpdated?.Invoke(scaledDeltaTime);
        }

        private void CompleteLoop()
        {
            remainingLoops--;

            if (!(remainingLoops <= 0))
            {
                onLoopCompleted?.Invoke(remainingLoops);
                Init();
                UpdateInternal(overdraft.Value);
                return;
            }

            if (remainingLoops == 0)
            {
                onLoopCompleted?.Invoke(remainingLoops);
            }

            updateController.UnsubscribeFromUpdate(this);

            playState = PlayState.Finished;

            onCompleted?.Invoke();

            if (updateController is Flow parentFlow)
            {
                parentFlow.CompleteUpdatable(this);
            }
        }

        internal void CompleteUpdatable(AbstractUpdatable updatable)
        {
            float overdraft = 0;
            if (updatable is AbstractAnimation animation)
            {
                overdraft = animation.OverDraft.Value;
                animation.OverDraft = null;
            }

            UpdatableWrapper nextAnimationWrapper = runningUpdatableWrappers[updatable.Id].next;
            runningUpdatableWrappers.Remove(updatable.Id);
            if (nextAnimationWrapper == null)
            {
                --runningUpdatableWrappersCount;
                if (runningUpdatableWrappersCount == 0 && nextTimeIndexedUpdatableWrapper == null)
                {
                    this.overdraft = overdraft;
                    CompleteLoop();
                }
                return;
            }

            updatable = nextAnimationWrapper.GetUpdatable();
            runningUpdatableWrappers.Add(updatable.Id, nextAnimationWrapper);
            updatable.StartInternal(overdraft);
        }

        #endregion

        #region QuickSort TimeIndex

        private void QuickSortByTimeIndex(UpdatableWrapper[] arr, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            int i = Partition(arr, start, end);
            QuickSortByTimeIndex(arr, start, i - 1);
            QuickSortByTimeIndex(arr, i + 1, end);
        }

        private int Partition(UpdatableWrapper[] arr, int start, int end)
        {
            UpdatableWrapper temp;
            float p = arr[end].timeIndex.Value;
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                if (arr[j].timeIndex < p)
                {
                    i++;
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            temp = arr[i + 1];
            arr[i + 1] = arr[end];
            arr[end] = temp;
            return i + 1;
        }

        #endregion

    }
}