using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace FlowEnt
{
    /// <summary>
    /// A tween is a simple interpolation from 0 to 1 which has several options and events attached.
    /// </summary>
    public sealed class Tween : AbstractAnimation,
        IFluentTweenOptionable<Tween>,
        IFluentTweenEventable<Tween>
    {
        public Tween(TweenOptions options) : base(options.AutoStart)
        {
            CopyOptions(options);
        }

        public Tween(bool autoStart = false) : base(autoStart)
        {
        }

        public Tween(float time, bool autoStart = false) : base(autoStart)
        {
            this.time = time;
        }

        private Action onStarting;
        private Action<float> onUpdating;
        private Action<float> onUpdated;
        private Action onLoopCompleted;

        #region Options

        private float time = 1;
        private LoopType loopType;
        private IEasing easing = TweenOptions.LinearEasing;

        #endregion

        private IMotion[] motions = new IMotion[0];


        #region Internal Members

        private int? remainingLoops;
        private float remainingTime;

        #endregion

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
            onStarted?.Invoke();
            playState = PlayState.Playing;

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

            bool isForward = loopType == LoopType.Reset || (loopCount - remainingLoops) % 2 == 0;
            float currentLoopTime = isForward ? time - remainingTime : remainingTime;
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
            if (loopCount == null)
            {
                onLoopCompleted?.Invoke();
                UpdateInternal(overdraft.Value);
            }

            remainingLoops--;
            if (remainingLoops > 0)
            {
                onLoopCompleted?.Invoke();
                UpdateInternal(overdraft.Value);
            }

            updateController.UnsubscribeFromUpdate(this);

            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnComplete();
            }

            onCompleted?.Invoke();

            if (updateController is Flow parentFlow)
            {
                parentFlow.CompleteAnimation(this);
            }

            playState = PlayState.Finished;
        }

        #endregion

        #region Setters

        #region Events

        public Tween OnStarting(Action callback)
        {
            onStarting += callback;
            return this;
        }

        public Tween OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        public Tween OnUpdating(Action<float> callback)
        {
            onUpdating += callback;
            return this;
        }

        public Tween OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        public Tween OnLoopCompleted(Action callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        public Tween OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }

        internal override void OnCompletedInternal(Action callback)
        {
            onCompleted += callback;
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

        #region Options

        public Tween SetOptions(TweenOptions options)
        {
            CopyOptions(options);
            return this;
        }

        public Tween SetOptions(Func<TweenOptions, TweenOptions> optionsBuilder)
        {
            CopyOptions(optionsBuilder(new TweenOptions()));
            return this;
        }

        public Tween SetSkipFrames(int frames)
        {

            this.skipFrames = frames;
            return this;
        }

        public Tween SetDelay(float time)
        {
            this.delay = time;
            return this;
        }

        public Tween SetTime(float time)
        {
            this.time = time;
            return this;
        }

        public Tween SetEasing(IEasing easing)
        {
            this.easing = easing;
            return this;
        }

        public Tween SetEasing(Easing easing)
        {
            this.easing = EasingFactory.Create(easing);
            return this;
        }

        public Tween SetEasing(AnimationCurve animationCurve)
        {
            this.easing = new AnimationCurveEasing(animationCurve);
            return this;
        }

        public Tween SetLoopType(LoopType loopType)
        {
            this.loopType = loopType;
            return this;
        }

        public Tween SetLoopCount(int? loopCount)
        {
            this.loopCount = loopCount;
            return this;
        }

        public Tween SetTimeScale(float timeScale)
        {
            if (timeScale < 0)
            {
                throw new ArgumentException("Value cannot be less than 0.");
            }
            this.timeScale = timeScale;
            return this;
        }

        private void CopyOptions(TweenOptions options)
        {
            skipFrames = options.SkipFrames;
            delay = options.Delay;
            time = options.Time;
            loopType = options.LoopType;
            loopCount = options.LoopCount;
            easing = options.Easing;
            timeScale = options.TimeScale;
        }

        #endregion

        #endregion

    }
}
