using System;
using System.Collections.Generic;

namespace FlowEnt
{
    public sealed class Flow : AbstractAnimation
    {
        public Flow(bool autoStart = false) : base(autoStart)
        {
            CurrentThread = new Thread();
            Threads.Add(CurrentThread);
        }

        private List<Thread> Threads { get; set; } = new List<Thread>();
        private List<Thread> RunningThreads { get; set; } = new List<Thread>();
        private Thread CurrentThread { get; set; }

        #region Internal Members

        private float time;

        #endregion

        #region Events

        protected override void OnAutoStart(float deltaTime)
        {
            if (PlayState != PlayState.Building)
            {
                return;
            }

            Start();
            Update(deltaTime);
        }

        public Flow Start()
        {

            return this;
        }

        public override void Update(float deltaTime)
        {
            time += deltaTime;


        }

        #endregion

        #region Setters

        public Flow After(AbstractAnimation animation)
        {
            CurrentThread.Animations.Enqueue(animation);
            return this;
        }

        public Flow At(float timeIndex, AbstractAnimation animation)
        {
            if (timeIndex < 0)
            {
                throw new ArgumentException($"Time index cannot be negative. Value: {timeIndex}");
            }
            CurrentThread = new Thread(timeIndex);
            Threads.Add(CurrentThread);
            CurrentThread.Animations.Enqueue(animation);
            return this;
        }

        #endregion

    }
}
