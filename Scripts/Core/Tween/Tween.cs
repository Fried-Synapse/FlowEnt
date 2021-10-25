using System;
using System.Linq;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// A tween is a simple interpolation from 0 to 1 which has several options and events attached.
    /// </summary>
    public sealed partial class Tween : AbstractAnimation,
        IFluentTweenOptionable<Tween>,
        IFluentTweenEventable<Tween>
    {
        private enum LoopDirection
        {
            Forward,
            Backward
        }

        public Tween(TweenOptions options)
        {
            CopyOptions(options);
        }

        public Tween(bool autoStart = false)
        {
            AutoStart = autoStart;
        }

        public Tween(float time, bool autoStart = false)
        {
            if (time < 0)
            {
                throw new ArgumentException(TweenOptions.ErrorTimeNegative);
            }
            if (float.IsInfinity(time))
            {
                throw new ArgumentException(TweenOptions.ErrorTimeInfinity);
            }
            AutoStart = autoStart;
            this.time = time;
        }

        private IMotion[] motions = new IMotion[0];
        private int? remainingLoops;
        private float remainingTime;
        private LoopDirection loopDirection;

        #region Lifecycle

        public Tween Start()
        {
            if (playState != PlayState.Building)
            {
                throw new FlowEntException("Tween already started.");
            }

            if (autoStartHelper != null)
            {
                CancelAutoStart();
            }
            StartInternal();
            return this;
        }

        public async Task<Tween> StartAsync()
        {
            if (playState != PlayState.Building)
            {
                throw new FlowEntException("Tween already started.");
            }

            if (autoStartHelper != null)
            {
                CancelAutoStart();
            }
            StartInternal();
            await new AwaitableAnimation(this);
            return this;
        }

        internal override void StartInternal(float deltaTime = 0)
        {
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
            remainingTime = time;

            updateController.SubscribeToUpdate(this);

            onStarting?.Invoke();
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnStart();
            }
            playState = PlayState.Playing;
            onStarted?.Invoke();

            UpdateInternal(deltaTime);
        }

        internal override void UpdateInternal(float deltaTime)
        {
            remainingTime -= deltaTime * timeScale;

            if (remainingTime < 0)
            {
                overdraft = -remainingTime;
                remainingTime = 0;
            }

            float currentLoopTime = loopDirection == LoopDirection.Forward ? time - remainingTime : remainingTime;
            float t = easing.GetValue(currentLoopTime / time);

            onUpdating?.Invoke(t);
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnUpdate(t);
            }
            onUpdated?.Invoke(t);

            if (overdraft != null)
            {
                CompleteLoop();
            }
        }

        private void CompleteLoop()
        {
            remainingTime = time;
            remainingLoops--;

            if (!(remainingLoops <= 0))
            {
                for (int i = 0; i < motions.Length; i++)
                {
                    motions[i].OnLoopComplete();
                }

                onLoopCompleted?.Invoke(remainingLoops);
                if (loopType == LoopType.PingPong)
                {
                    loopDirection = loopDirection == LoopDirection.Forward ? LoopDirection.Backward : LoopDirection.Forward;
                }
                float overdraft = this.overdraft.Value;
                this.overdraft = null;
                UpdateInternal(overdraft);
                return;
            }

            if (remainingLoops == 0)
            {
                onLoopCompleted?.Invoke(remainingLoops);
            }

            updateController.UnsubscribeFromUpdate(this);

            onCompleting?.Invoke();
            playState = PlayState.Finished;
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnComplete();
            }
            onCompleted?.Invoke();

            if (updateController is Flow parentFlow)
            {
                parentFlow.CompleteUpdatable(this);
            }
        }

        #endregion

        #region Motions

        public Tween Apply(IMotion motion)
        {
            motions = motions.Append(motion).ToArray();
            return this;
        }

        public TweenMotion<T> For<T>(T element)
        {
            return new TweenMotion<T>(this, element);
        }

        #endregion

    }
}
