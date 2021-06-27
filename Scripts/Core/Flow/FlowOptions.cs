using System;

namespace FlowEnt
{
    internal interface IFluentFlowOptionable<T>
    {
        T SetLoopCount(int? loopCount);

        T SetTimeScale(float timeScale);
    }
    public class FlowOptions : AbstractAnimationOptions, IFluentFlowOptionable<FlowOptions>
    {
        public FlowOptions(bool autoStart = false) : base(autoStart)
        {
        }

        public int? LoopCount { get; set; } = 1;
        private float timeScale = 1;

        public float TimeScale
        {
            get { return timeScale; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Value cannot be less than 0");
                }
                timeScale = value;
            }
        }

        #region Options

        public FlowOptions SetLoopCount(int? loopCount)
        {
            LoopCount = loopCount;
            return this;
        }

        public FlowOptions SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            return this;
        }

        #endregion

    }
}
