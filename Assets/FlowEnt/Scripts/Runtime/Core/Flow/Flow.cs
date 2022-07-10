using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides functionality to create a sequence or multiple sequences of animations.
    /// For more information please go to https://flowent.friedsynapse.com/flow
    /// </summary>
    public sealed partial class Flow : AbstractAnimation,
        IUpdateController,
        IFluentControllable<Flow>
    {
        private const string ErrorAnimationAlreadyStarted = "Cannot add animation that has already started.";

        /// <summary>
        /// Creates a new flow using the options provided.
        /// </summary>
        /// <param name="options"></param>
        public Flow(FlowOptions options)
        {
            SetOptions(options);
        }

        /// <summary>
        /// Creates a new flow.
        /// </summary>
        /// <param name="autoStart">Whether the flow should start automatically or not.</param>
        public Flow(bool autoStart = AbstractAnimationOptions.DefaultAutoStart)
        {
            AutoStart = autoStart;
        }

        #region Internal Members

        private readonly UpdatablesFastList<AbstractUpdatable> updatables = new UpdatablesFastList<AbstractUpdatable>();
        private readonly List<AbstractUpdatableWrapper> updatableWrappersQueue = new List<AbstractUpdatableWrapper>(2);
        private AbstractUpdatableWrapper lastQueuedUpdatableWrapper;
        private readonly Dictionary<ulong, AbstractUpdatableWrapper> runningUpdatableWrappers = new Dictionary<ulong, AbstractUpdatableWrapper>(2);
        private int runningUpdatableWrappersCount;
        private int? remainingLoops;

        #endregion

        #region ISeekable

        private protected override bool IsSeekable => false;
        private protected override float TotalSeekTime => throw new NotImplementedException();
        private protected override float GetDeltaTimeFromRatio(float ratio)
            => throw new NotImplementedException();

        #endregion

        #region Controls

        /// <inheritdoc cref="AbstractAnimation.Start" />
        /// \copydoc AbstractAnimation.Start
        /// <exception cref="AnimationException">If the flow has already started.</exception>
        public new Flow Start()
        {
            base.Start();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.StartAsync(CancellationToken?)" />
        /// \copydoc AbstractAnimation..StartAsync(CancellationToken?)
        /// <exception cref="AnimationException">If the flow has already started.</exception>
        public new async Task<Flow> StartAsync(CancellationToken? token = null)
        {
            await base.StartAsync(token);
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Resume" />
        /// \copydoc AbstractAnimation.Resume
        public new Flow Resume()
        {
            base.Resume();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Pause" />
        /// \copydoc AbstractAnimation.Pause
        public new Flow Pause()
        {
            base.Pause();
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Stop(bool)" />
        /// \copydoc AbstractUpdatable.Stop(bool)
        public new Flow Stop(bool triggerOnCompleted = false)
        {
            StopInternal(triggerOnCompleted);
            return this;
        }

        /// <inheritdoc cref="AbstractAnimation.Reset" />
        /// \copydoc AbstractAnimation.Reset
        /// <exception cref="AnimationException">If the flow is not finished.</exception>
        public new Flow Reset()
        {
            ResetInternal();
            return this;
        }

        #endregion

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

        internal override void StartInternal(float deltaTime = 0)
        {
            if (lastQueuedUpdatableWrapper == null)
            {
                throw new AnimationException(this, "Cannot start empty flow.");
            }

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
            onStarting?.Invoke();

            updateController.SubscribeToUpdate(this);
            playState = PlayState.Playing;
            onStarted?.Invoke();
            onLoopStarted?.Invoke(remainingLoops);

            StartUpdatables(deltaTime);
            FirstUpdateInternal(deltaTime);
        }

        private void StartUpdatables(float deltaTime)
        {
            int count = updatableWrappersQueue.Count;
            runningUpdatableWrappersCount = count;
            for (int i = 0; i < count; i++)
            {
                AbstractUpdatableWrapper updatableWrapper = updatableWrappersQueue[i];
                AbstractUpdatable updatable = updatableWrapper.GetUpdatable();
                runningUpdatableWrappers.Add(updatable.Id, updatableWrapper);
                updatable.StartInternal(deltaTime);
            }
        }

        private void FirstUpdateInternal(float deltaTime)
        {
            float scaledDeltaTime = deltaTime * timeScale;
            elapsedTime += scaledDeltaTime;

            onUpdating?.Invoke(scaledDeltaTime);
            onUpdated?.Invoke(scaledDeltaTime);
        }

        internal override void UpdateInternal(float deltaTime)
        {
            float scaledDeltaTime = deltaTime * timeScale;
            elapsedTime += scaledDeltaTime;

            onUpdating?.Invoke(scaledDeltaTime);

            AbstractUpdatable index = updatables.anchor.next;

            while (index != null)
            {
                index.UpdateInternal(scaledDeltaTime);
                index = index.next;
            }

            onUpdated?.Invoke(scaledDeltaTime);
        }

        private void CompleteLoop()
        {
            remainingLoops--;

            if (!(remainingLoops <= 0))
            {
                elapsedTime = 0;
                onLoopCompleted?.Invoke(remainingLoops);
                ResetUpdatables();
                onLoopStarted?.Invoke(remainingLoops);
                StartUpdatables(overdraft.Value);
                FirstUpdateInternal(overdraft.Value);
                return;
            }

            if (remainingLoops == 0)
            {
                onLoopCompleted?.Invoke(remainingLoops);
            }

            updateController.UnsubscribeFromUpdate(this);

            onCompleting?.Invoke();

            playState = PlayState.Finished;

            onCompleted?.Invoke();

            if (updateController is Flow parentFlow)
            {
                parentFlow.CompleteUpdatable(this);
            }
        }

        internal void CompleteUpdatable(AbstractUpdatable updatable)
        {
            float overdraft = updatable switch
            {
                AbstractAnimation animation => animation.Overdraft.Value,
                DelayFlowAwaiter delayAwaiter => delayAwaiter.Overdraft,
                _ => 0f
            };

            AbstractUpdatableWrapper nextAnimationWrapper = runningUpdatableWrappers[updatable.Id].next;
            runningUpdatableWrappers.Remove(updatable.Id);
            if (nextAnimationWrapper == null)
            {
                --runningUpdatableWrappersCount;
                if (runningUpdatableWrappersCount == 0)
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

        protected override void ResetInternal()
        {
            base.ResetInternal();
            remainingLoops = 0;
            runningUpdatableWrappersCount = 0;
            runningUpdatableWrappers.Clear();
            updatables.Clear();
            ResetUpdatables();
        }

        private void ResetUpdatables()
        {
            for (int i = 0; i < updatableWrappersQueue.Count; i++)
            {
                AbstractUpdatableWrapper updatableWrapper = updatableWrappersQueue[i];
                do
                {
                    AbstractUpdatable updatable = updatableWrapper.GetUpdatable();
                    if (updatable is AbstractAnimation animation && animation.PlayState != PlayState.Finished)
                    {
                        animation.Stop();
                    }
                    updatable.Reset();
                    updatableWrapper = updatableWrapper.next;
                } while (updatableWrapper != null);
            }
        }

        #endregion
    }
}