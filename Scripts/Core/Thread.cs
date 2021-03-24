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
        internal bool HasInfiniteMotionLoops
        {
            get
            {
                for (int i = 0; i < Motions.Count; i++)
                {
                    if (Motions[i].HasInfiniteLoop)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private List<Motion> Motions { get; } = new List<Motion>();
        private Queue<Motion> PlayingMotions { get; set; }

        private Motion PlayingMotion { get; set; }

        #region Build

        public Motion Enqueue(float time)
        {
            if (time <= 0)
            {
                throw new ArgumentException($"{time} is not a valid time frame to enqueue a motion.");
            }
            Motion motion = new Motion(this, time);
            Motions.Add(motion);
            return motion;
        }

        #endregion

        #region Lifecycle

        internal void Init()
        {
            PlayingMotions = new Queue<Motion>(Motions);
        }

        internal void Update(float deltaTime)
        {
            if (PlayingMotion == null)
            {
                if (PlayingMotions.Count > 0)
                {
                    PlayingMotion = PlayingMotions.Dequeue();
                    PlayingMotion.InitPlay();
                }
                else
                {
                    OnComplete?.Invoke(deltaTime);
                    return;
                }
            }

            float remainingDeltaTime = PlayingMotion.Update(deltaTime);
            if (remainingDeltaTime > 0)
            {
                PlayingMotion = null;
                Update(deltaTime);
            }

        }

        internal void Reset()
        {
            for (int i = 0; i < Motions.Count; i++)
            {
                Motions[i].Reset();
            }
        }

        #endregion
    }
}
