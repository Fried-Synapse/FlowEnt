using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides common options for animations.
    /// </summary>
    [Serializable]
    public class AbstractAnimationOptions
    {
        internal const string ErrorLoopCountNegative = "Value cannot be 0 or less. If you want to set an infinite loop set the value to null.";
        internal const string ErrorTimeScaleNegative = "Value cannot be less than 0.";

        /// <summary>
        /// Initialises a new instance of the <see cref="AbstractAnimationOptions"/> class.
        /// </summary>
        public AbstractAnimationOptions()
        {
        }

        [SerializeField]
        private bool autoStart;
        /// <summary>
        /// Whether the animation should auto start or not. If set to false, you need to start the animation manually.
        /// </summary>
        /// <remarks>
        /// AutoStart for an animation requires to have a helper that looks for the next frame therefore manually starting the Animation will always be more efficient.
        /// </remarks>
        public bool AutoStart { get => autoStart; set => autoStart = value; }

        [SerializeField]
        private int skipFrames;
        /// <summary>
        /// The amount of frames that the animation will skip from the moment it started till the animation begins.
        /// </summary>
        public int SkipFrames { get => skipFrames; set => skipFrames = value; }

        [SerializeField]
        private float delay;
        /// <summary>
        /// The amount of time that the animation will skip from the moment it started till the animation begins.
        /// </summary>
        public float Delay { get => delay; set => delay = value; }

        [SerializeField]
        private float timeScale = 1;
        /// <summary>
        /// The scale of the time that will be applied to the animation.
        /// </summary>
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

        [SerializeField]
        private int? loopCount = 1;

        /// <summary>
        /// The number of loops the animation will run. Use <b>null</b> for infinite loops.
        /// </summary>
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
    }
}
