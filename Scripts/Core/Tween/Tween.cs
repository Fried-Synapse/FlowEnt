using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace FlowEnt
{
    internal interface IFluentTweenEventable<T>
    {
        T OnBeforeStart(Action callback);

        T OnAfterStart(Action callback);

        T OnBeforeUpdate(Action<float> callback);

        T OnAfterUpdate(Action<float> callback);

        T OnBeforeComplete(Action callback);

        T OnAfterComplete(Action callback);
    }

    public sealed class Tween : AbstractAnimation,
        IFluentTweenOptionable<Tween>,
        IFluentTweenEventable<Tween>
    {
        public Tween(TweenOptions options) : base(options.AutoStart)
        {
            CopyOptions(options);
        }

        public Tween(bool autoStart = false) : this(new TweenOptions() { AutoStart = autoStart })
        {
        }

        public Tween(float time, bool autoStart = false) : this(new TweenOptions() { Time = time, AutoStart = autoStart })
        {
        }

        private Action OnBeforeStartCallback { get; set; }
        private Action OnAfterStartCallback { get; set; }
        private Action<float> OnBeforeUpdateCallback { get; set; }
        private Action<float> OnAfterUpdateCallback { get; set; }
        private Action OnBeforeCompleteCallback { get; set; }
        private Action OnAfterCompleteCallback { get; set; }

        #region Options

        private float time;
        private LoopType loopType;
        private int? loopCount;
        private IEasing easing;
        private float timeScale;

        #endregion

        private IMotion[] motions = new IMotion[0];


        #region Internal Members

        private int? remainingLoops;
        private float remainingTime;

        #endregion

        #region Lifecycle

        protected override void OnAutoStart(float deltaTime)
        {
            if (PlayState != PlayState.Building)
            {
                return;
            }

            Start();
            UpdateInternal(deltaTime);
        }

        public Tween Start()
        {
            StartInternal();
            return this;
        }

        public async Task<Tween> StartAsync()
        {
            StartInternal();
            await new AwaitableAnimation(this);
            return this;
        }

        internal override void StartInternal(bool subscribeToUpdate = true)
        {
            remainingLoops = loopCount;
            remainingTime = time;

            IsSubscribedToUpdate = subscribeToUpdate;
            if (IsSubscribedToUpdate)
            {
                FlowEntController.Instance.SubscribeToUpdate(this);
            }
            OnBeforeStartCallback?.Invoke();
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnStart();
            }
            OnAfterStartCallback?.Invoke();
            PlayState = PlayState.Playing;
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

            OnBeforeUpdateCallback?.Invoke(t);
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnUpdate(t);
            }
            OnAfterUpdateCallback?.Invoke(t);

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
                UpdateInternal(overdraft);
                return null;
            }

            remainingLoops--;
            if (remainingLoops > 0)
            {
                UpdateInternal(overdraft);
                return null;
            }

            if (IsSubscribedToUpdate)
            {
                FlowEntController.Instance.UnsubscribeFromUpdate(this);
            }

            OnBeforeCompleteCallback?.Invoke();
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnComplete();
            }
            OnAfterCompleteCallback?.Invoke();
            PlayState = PlayState.Finished;
            return overdraft;
        }

        #endregion

        #region Setters

        #region Events

        public Tween OnBeforeStart(Action callback)
        {
            OnBeforeStartCallback += callback;
            return this;
        }

        public Tween OnAfterStart(Action callback)
        {
            OnAfterStartCallback += callback;
            return this;
        }

        public Tween OnBeforeUpdate(Action<float> callback)
        {
            OnBeforeUpdateCallback += callback;
            return this;
        }

        public Tween OnAfterUpdate(Action<float> callback)
        {
            OnAfterUpdateCallback += callback;
            return this;
        }

        public Tween OnBeforeComplete(Action callback)
        {
            OnBeforeCompleteCallback += callback;
            return this;
        }

        public Tween OnAfterComplete(Action callback)
        {
            OnAfterCompleteCallback += callback;
            return this;
        }

        protected override void OnCompleteInternal(Action callback)
        {
            OnAfterCompleteCallback += callback;
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
                throw new ArgumentException("Value cannot be less than 0");
            }
            this.timeScale = timeScale;
            return this;
        }

        private void CopyOptions(TweenOptions options)
        {
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
