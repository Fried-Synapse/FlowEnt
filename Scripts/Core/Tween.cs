using System;
using System.Linq;
using System.Threading.Tasks;

namespace FlowEnt
{
    public class TweenOptions
    {
        public bool AutoStart { get; set; }
        public float Time { get; set; }
        public LoopType LoopType { get; set; } = LoopType.Reset;
        public int? LoopCount { get; set; } = 1;
        public IEasing Easing { get; set; }
    }

    public sealed class Tween : AbstractAnimation
    {
        public Tween(TweenOptions options) : base(options.AutoStart)
        {
            Options = options;
        }

        public Tween(float time = 1f, bool autoStart = false) : this(new TweenOptions() { Time = time, AutoStart = autoStart })
        {
        }

        private Action<float> OnUpdateCallback { get; set; }

        #region Settings Properties

        private TweenOptions Options { get; }
        private IMotion[] Motions { get; set; } = new IMotion[0];

        #endregion

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
            remainingLoops = Options.LoopCount;
            remainingTime = Options.Time;
            if (Options.Easing == null)
            {
                Options.Easing = new LinearEasing();
            }

            IsSubscribedToUpdate = subscribeToUpdate;
            if (IsSubscribedToUpdate)
            {
                FlowEntController.Instance.SubscribeToUpdate(this);
            }
            OnStartCallback?.Invoke();
            for (int i = 0; i < Motions.Length; i++)
            {
                Motions[i].OnStart();
            }
            PlayState = PlayState.Playing;
        }

        internal override float? UpdateInternal(float deltaTime)
        {
            remainingTime -= deltaTime;

            float? overdraft = null;

            if (remainingTime < 0)
            {
                overdraft = -remainingTime;
                remainingTime = 0;
            }

            bool isForward = Options.LoopType == LoopType.Reset || (Options.LoopCount - remainingLoops) % 2 == 0;
            float currentLoopTime = isForward ? Options.Time - remainingTime : remainingTime;
            float t = Options.Easing.GetValue(currentLoopTime / Options.Time);

#if FlowEnt_Debug
            UnityEngine.Debug.Log($"{UnityEngine.Time.time, -12}:   {Id} - {currentLoopDelta / Time}");
#endif

            OnUpdateCallback?.Invoke(t);
            for (int i = 0; i < Motions.Length; i++)
            {
                Motions[i].OnUpdate(t);
            }

            if (overdraft != null)
            {
                return CompleteLoop(overdraft.Value);
            }
            return null;
        }

        private float? CompleteLoop(float overdraft)
        {
            remainingTime = Options.Time;
            if (Options.LoopCount == null)
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
            for (int i = 0; i < Motions.Length; i++)
            {
                Motions[i].OnComplete();
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

        public Tween OnComplete(Action callback)
        {
            OnCompleteCallback += callback;
            return this;
        }

        #endregion

        #region Motions

        public Tween Apply(IMotion motion)
        {
            Motions = Motions.Append(motion).ToArray();
            return this;
        }

        public MotionWrapper<T> For<T>(T element)
        {
            return new MotionWrapper<T>(this, element);
        }

        #endregion

        #region Settings

        public Tween SetTime(float time)
        {
            Options.Time = time;
            return this;
        }

        public Tween SetLoopType(LoopType loopType)
        {
            Options.LoopType = loopType;
            return this;
        }

        public Tween SetLoopCount(int? loopCount)
        {
            Options.LoopCount = loopCount;
            return this;
        }

        #endregion

        #endregion

    }


}
