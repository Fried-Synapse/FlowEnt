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
                for (int i = 0; i < Animations.Count; i++)
                {
                    //if (Tweens[i].HasInfiniteLoop)
                    //{
                    //    return true;
                    //}
                }
                return false;
            }
        }

        private List<AbstractAnimation> Animations { get; } = new List<AbstractAnimation>();
        private Queue<AbstractAnimation> PlayingAnimations { get; set; }

        private AbstractAnimation PlayingTween { get; set; }

        #region Build

        public Tween Enqueue(float time)
        {
            if (time <= 0)
            {
                throw new ArgumentException($"{time} is not a valid time frame to enqueue a motion.");
            }
            Tween motion = new Tween(time);
            Animations.Add(motion);
            return motion;
        }

        #endregion

        #region Lifecycle

        internal void Init()
        {
            PlayingAnimations = new Queue<AbstractAnimation>(Animations);
        }

        internal void Update(float deltaTime)
        {
            if (PlayingTween == null)
            {
                if (PlayingAnimations.Count > 0)
                {
                    PlayingTween = PlayingAnimations.Dequeue();
                    PlayingTween.Start();
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

        #endregion
    }
}
