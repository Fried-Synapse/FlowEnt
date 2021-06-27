using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace FlowEnt
{
    public sealed class Tween : AbstractAnimation, IFluentTweenOptionable<Tween>
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

        private Action<float> OnUpdateCallback { get; set; }

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
            OnStartCallback?.Invoke();
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnStart();
            }
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

#if FlowEnt_Debug
            UnityEngine.Debug.Log($"{UnityEngine.Time.time, -12}:   {Id} - {currentLoopDelta / Time}");
#endif

            OnUpdateCallback?.Invoke(t);
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnUpdate(t);
            }

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

            OnCompleteCallback?.Invoke();
            for (int i = 0; i < motions.Length; i++)
            {
                motions[i].OnComplete();
            }
            PlayState = PlayState.Finished;
            return overdraft;
        }

        #endregion

        #region Setters

        #region Events

        public Tween OnStart(Action callback)
        {
            OnStartCallback += callback;
            return this;
        }

        public Tween OnUpdate(Action<float> callback)
        {
            OnUpdateCallback += callback;
            return this;
        }

        public new Tween OnComplete(Action callback)
        {
            OnCompleteCallback += callback;
            return this;
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
