using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlowEnt
{
    public class Tween : FlowEntObject
    {
        public Tween(Thread thread, float time)
        {
            Thread = thread;
            Time = time;
        }

        #region Reference Properties

        public Thread Thread { get; }
        public Flow Flow => Thread.Flow;
        internal bool HasInfiniteLoop => LoopCount < 0;

        #endregion

        #region Events

        protected Action OnStartCallback { get; set; }
        protected Action<float> OnUpdateCallback { get; set; }
        protected Action OnCompleteCallback { get; set; }

        #endregion

        #region Settings Properties

        protected float Time { get; }
        protected LoopType LoopType { get; set; } = LoopType.Reset;
        protected int LoopCount { get; set; } = 1;
        protected IEasing Easing { get; set; }
        protected List<IMotion> Motions { get; set; } = new List<IMotion>();

        #endregion

        #region Internal Properties

        protected float PlayedTime { get; private set; }
        protected float? TimeToPlay { get; private set; }

        #endregion

        #region Build

        public Tween Enqueue(float time)
            => Thread.Enqueue(time);

        public Tween Loop(LoopType loopType = LoopType.Reset, int loopCount = 1)
        {
            LoopType = loopType;
            LoopCount = loopCount;
            return this;
        }

        /// <summary>
        /// Sets a predefined ease. You can check the easings list at https://easings.net/.
        /// </summary>
        /// <param name="easing">The easing type</param>
        /// <returns></returns>
        public Tween SetEase(Easing easing)
        {
            Easing = EasingFactory.Create(easing);
            return this;
        }

        public Tween SetEase(IEasing easing)
        {
            Easing = easing;
            return this;
        }

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

        public MotionWrapper<T> For<T>(T element)
            => new MotionWrapper<T>(this, element);

        public Tween Apply(IMotion motion)
        {
            Motions.Add(motion);
            return this;
        }

        public Flow Concurrent()
            => Flow.Concurrent();

        public Flow Play(int loopCount = 1)
            => Flow.Play(loopCount);

        public async Task<Flow> PlayAsync(int loopCount = 1)
            => await Flow.PlayAsync(loopCount);

        #endregion

        #region Lifecycle

        internal void InitPlay()
        {
            if (LoopCount >= 0)
            {
                TimeToPlay = Time * LoopCount;
            }
            if (Easing == null)
            {
                Easing = new LinearEasing();
            }
            OnStart();
        }

        internal void OnStart()
        {
            OnStartCallback?.Invoke();
            for (int i = 0; i < Motions.Count; i++)
            {
                Motions[i].OnStart();
            }
        }

        internal float Update(float deltaTime)
        {
            PlayedTime += deltaTime;
            float overdraft = -1;

            if (TimeToPlay != null)
            {
                if (PlayedTime > TimeToPlay)
                {
                    overdraft = PlayedTime - TimeToPlay.Value;
                    PlayedTime = TimeToPlay.Value;
                }
            }

            float currentLoopDelta = PlayedTime == Time ? Time : PlayedTime % Time;
            switch (LoopType)
            {
                case LoopType.Reset:
                    break;
                case LoopType.PingPong:
                    if (((int)(PlayedTime / Time)) % 2 == 1)
                    {
                        currentLoopDelta = Time - currentLoopDelta;
                    }
                    break;
                default:
                    throw new FlowEntException(this, "Unknown loop type.");
            }
            float t = Easing.GetValue(currentLoopDelta / Time);

#if FlowEnt_Debug
            UnityEngine.Debug.Log($"{UnityEngine.Time.time, -12}:   {Id} - {currentLoopDelta / Time}");
#endif

            OnUpdateCallback?.Invoke(t);
            for (int i = 0; i < Motions.Count; i++)
            {
                Motions[i].OnUpdate(t);
            }

            if (overdraft > 0)
            {
                OnCompleteCallback?.Invoke();
                for (int i = 0; i < Motions.Count; i++)
                {
                    Motions[i].OnComplete();
                }
            }
            return overdraft;
        }

        internal void Reset()
        {
            PlayedTime = 0;
        }

        #endregion
    }
}