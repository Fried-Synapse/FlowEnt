using System;

namespace FlowEnt
{
    public class AbstractAnimationOptions
    {
        internal const string ErrorLoopCountNegative = "Value cannot be 0 or less. If you want to set an infinite loop set the value to null.";
        internal const string ErrorTimeScaleNegative = "Value cannot be less than 0.";
        public bool AutoStart { get; set; }

        public AbstractAnimationOptions(bool autoStart = false)
        {
            AutoStart = autoStart;
        }

        public int SkipFrames { get; set; }
        public float Delay { get; set; } = -1f;
        private int? loopCount = 1;
        public int? LoopCount
        {
            get { return loopCount; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ErrorLoopCountNegative);
                }
                loopCount = value;
            }
        }
        private float timeScale = 1;

        public float TimeScale
        {
            get { return timeScale; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ErrorTimeScaleNegative);
                }
                timeScale = value;
            }
        }
    }
}
