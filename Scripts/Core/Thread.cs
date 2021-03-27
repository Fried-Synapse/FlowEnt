using System;
using System.Collections.Generic;

namespace FlowEnt
{
    public class Thread : FlowEntObject
    {
        internal Thread(Flow flow)
        {
            Flow = flow;
        }

        public Flow Flow { get; }
        internal Action<float> OnComplete { get; set; }
        internal bool HasInfiniteTweenLoops
        {
            get
            {
                for (int i = 0; i < Tweens.Count; i++)
                {
                    if (Tweens[i].HasInfiniteLoop)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private List<Tween> Tweens { get; } = new List<Tween>();
        private Queue<Tween> PlayingTweens { get; set; }

        private Tween PlayingTween { get; set; }

        #region Build

        public Tween Enqueue(float time)
        {
            if (time <= 0)
            {
                throw new ArgumentException($"{time} is not a valid time frame to enqueue a motion.");
            }
            Tween motion = new Tween(this, time);
            Tweens.Add(motion);
            return motion;
        }

        #endregion

        #region Lifecycle

        internal void Init()
        {
            PlayingTweens = new Queue<Tween>(Tweens);
        }

        internal void Update(float deltaTime)
        {
            if (PlayingTween == null)
            {
                if (PlayingTweens.Count > 0)
                {
                    PlayingTween = PlayingTweens.Dequeue();
                    PlayingTween.InitPlay();
                }
                else
                {
                    OnComplete?.Invoke(deltaTime);
                    return;
                }
            }

            float remainingDeltaTime = PlayingTween.Update(deltaTime);
            if (remainingDeltaTime > 0)
            {
                PlayingTween = null;
                Update(remainingDeltaTime);
            }
        }

        internal void Reset()
        {
            for (int i = 0; i < Tweens.Count; i++)
            {
                Tweens[i].Reset();
            }
        }

        #endregion
    }
}
