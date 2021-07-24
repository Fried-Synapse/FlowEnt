using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace FlowEnt
{
    internal interface IFluentTweenEventable<T>
    {
        T OnStarting(Action callback);
        T OnStarted(Action callback);

        T OnUpdating(Action<float> callback);
        T OnUpdated(Action<float> callback);

        T OnLoopCompleted(Action callback);

        T OnCompleted(Action callback);
    }

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
            if (PlayState != PlayState.Building)
            {
                throw new FlowEntException("Tween already started.");
            }

            if (AutoStartHelper != null)
            {
                CancelAutoStart();
            }
            StartInternal();
            return this;
        }

        public async Task<Tween> StartAsync()
        {
            if (PlayState != PlayState.Building)
            {
                throw new FlowEntException("Tween already started.");
            }

            if (AutoStartHelper != null)
            {
                CancelAutoStart();
            }
            StartInternal();
            await new AwaitableAnimation(this);
            return this;
        }

        internal override void StartInternal(bool subscribeToUpdate = true, float? deltaTime = null)
        {
            // if (skipFrames > 0)
            // {
            //     StartSkipFrames(subscribeToUpdate);
            //     return;
            // }

            // if (delay > 0f)
            // {
            //     StartDelay(subscribeToUpdate);
            //     return;
            // }

            remainingLoops = loopCount;
            remainingTime = time;

            if (subscribeToUpdate)
            {
                FlowEntController.Instance.SubscribeToUpdate(this);
                IsSubscribedToUpdate = subscribeToUpdate;
            }
            onStarting?.Invoke();
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnStart();
            }
            onStarted?.Invoke();
            PlayState = PlayState.Playing;

            if (deltaTime != null)
            {
                UpdateInternal(deltaTime.Value);
            }
        }

        internal override float? UpdateInternal(float deltaTime)
        {
            remainingTime -= deltaTime * timeScale;

            float? overdraft = null;

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
                return CompleteLoop(overdraft.Value);
            }
            return null;
        }

        private float? CompleteLoop(float overdraft)
        {
            remainingTime = time;
            if (loopCount == null)
            {
                onLoopCompleted?.Invoke();
                UpdateInternal(overdraft);
                return null;
            }

            remainingLoops--;
            if (remainingLoops > 0)
            {
                onLoopCompleted?.Invoke();
                UpdateInternal(overdraft);
                return null;
            }

            if (IsSubscribedToUpdate)
            {
                FlowEntController.Instance.UnsubscribeFromUpdate(this);
            }

            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnComplete();
            }
            onCompleted?.Invoke();
            PlayState = PlayState.Finished;
            return overdraft;
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
