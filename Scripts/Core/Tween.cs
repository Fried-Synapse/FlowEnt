using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FlowEnt
{
    public class Tween : AbstractFlow, IUpdatable
    {
        private class AutoStartHelper : IUpdatable
        {
            public AutoStartHelper(Action<float> callback)
            {
                Callback = callback;
            }

            public int UpdateIndex { get ; set ; }
            private Action<float> Callback { get; set; }

            public void Update(float deltaTime)
            {
                FlowEntController.Instance.UnsubscribeFromUpdate(this);
                Callback.Invoke(deltaTime);
            }
        }

        public Tween(float time = 1f, bool autoStart = false)
        {
            Time = time;
            AutoStart = autoStart;
            if (AutoStart)
            {
                FlowEntController.Instance.SubscribeToUpdate(new AutoStartHelper(AutoStartUpdate));
            }
        }

        private Action OnStartCallback { get; set; }
        private Action<float> OnUpdateCallback { get; set; }
        private Action OnCompleteCallback { get; set; }

        protected AbstractFlow Next { get; private set; }

        #region Settings Properties

        public PlayState PlayState { get; private set; } = PlayState.Building;
        protected bool AutoStart { get; }
        protected float Time { get; }
        protected LoopType LoopType { get; set; } = LoopType.Reset;
        protected int? LoopCount { get; set; } = 1;
        protected IEasing Easing { get; set; }
        protected IMotion[] Motions { get; set; } = new IMotion[0];

        #endregion

        #region Internal Members

        private int? remainingLoops;
        private float remainingTime;
        public int UpdateIndex { get; set; }
        #endregion

        #region Events

        private void AutoStartUpdate(float deltaTime)
        {
            if (PlayState != PlayState.Building)
            {
                return;
            }

            Start();
            Update(deltaTime);
        }

        public Tween Start()
        {
            remainingLoops = LoopCount;
            remainingTime = Time;
            if (Easing == null)
            {
                Easing = new LinearEasing();
            }

            FlowEntController.Instance.SubscribeToUpdate(this);
            OnStartCallback?.Invoke();
            for (int i = 0; i < Motions.Length; i++)
            {
                Motions[i].OnStart();
            }
            PlayState = PlayState.Playing;
            return this;
        }

        /// <summary>
        /// Plays the flow with the number of specified loops.
        /// </summary>
        /// <param name="loopCount">The number of loops to be played. If number smaller than 0 it'll loop forever.</param>
        /// <returns>The current flow.</returns>
        public async Task<Tween> StartAsync()
        {
            Start();
            await new AwaitableTween(this);
            return this;
        }

        public void Update(float deltaTime)
        {
            remainingTime -= deltaTime;

            float? overdraft = null;

            if (remainingTime < 0)
            {
                overdraft = -remainingTime;
                remainingTime = 0;
            }

            bool isForward = LoopType == LoopType.Reset || (LoopCount - remainingLoops) % 2 == 0;
            float currentLoopTime = isForward ? Time - remainingTime : remainingTime;
            float t = Easing.GetValue(currentLoopTime / Time);

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
                CompleteLoop(overdraft.Value);
            }
        }

        private void CompleteLoop(float overdraft)
        {
            remainingTime = Time;
            if (LoopCount == null)
            {
                Update(overdraft);
                return;
            }

            remainingLoops--;
            if (remainingLoops > 0)
            {
                Update(overdraft);
                return;
            }

            FlowEntController.Instance.UnsubscribeFromUpdate(this);
            OnCompleteCallback?.Invoke();
            for (int i = 0; i < Motions.Length; i++)
            {
                Motions[i].OnComplete();
            }
            PlayState = PlayState.Finished;
        }

        #endregion

        #region Setters

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

        public Tween Apply(IMotion motion)
        {
            Motions = Motions.Append(motion).ToArray();
            return this;
        }

        public MotionWrapper<T> For<T>(T element)
        {
            return new MotionWrapper<T>(this, element);
        }

        public Tween SetLoopType(LoopType loopType)
        {
            LoopType = loopType;
            return this;
        }

        public Tween SetLoopCount(int loopCount)
        {
            LoopCount = loopCount;
            return this;
        }

        #endregion

    }

    internal class AwaitableTween
    {
        public AwaitableTween(Tween tween)
        {
            Tween = tween;
        }

        public Tween Tween { get; }
        public TweenAwaiter GetAwaiter()
            => new TweenAwaiter(Tween);
    }

    internal class TweenAwaiter : INotifyCompletion
    {
        public TweenAwaiter(Tween tween)
        {
            Tween = tween;
            Tween.OnComplete(() => OnCompletedCallback.Invoke());
        }

        public Tween Tween { get; }
        public bool IsCompleted => Tween.PlayState == PlayState.Finished;
        private Action OnCompletedCallback { get; set; }

        public Tween GetResult()
            => Tween;

        public void OnCompleted(Action continuation)
        {
            OnCompletedCallback = continuation;
        }
    }
}
