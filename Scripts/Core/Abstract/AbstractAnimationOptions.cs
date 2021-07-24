using System;

namespace FlowEnt
{
    public class AbstractAnimationOptions
    {
        public bool AutoStart { get; set; }

        public AbstractAnimationOptions(bool autoStart = false)
        {
            AutoStart = autoStart;
        }

        public int SkipFrames { get; set; }
        public float Delay { get; set; } = -1f;
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
    }
}
