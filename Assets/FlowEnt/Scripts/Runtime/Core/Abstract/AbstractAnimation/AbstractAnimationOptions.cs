using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides common options for animations.
    /// </summary>
    public class AbstractAnimationOptions : IFluentAnimationOptionable<AbstractAnimationOptions>
    {
        internal const string ErrorLoopCountNegative =
            "Value cannot be 0 or less. If you want to set an infinite loop set the value to null.";

        internal const string ErrorTimeScaleNegative = "Value cannot be less than 0.";
        internal const bool DefaultAutoStart = false;
        public const int DefaultLoopCount = 1;
        public const float DefaultTimeScale = 1f;

        /// <summary>
        /// Initialises a new instance of the <see cref="AbstractAnimationOptions"/> class.
        /// </summary>
        public AbstractAnimationOptions()
        {
        }

        /// <summary>
        /// The name of the animation.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The update type of the animation.
        /// </summary>
        public UpdateType UpdateType { get; set; }

        /// <summary>
        /// Whether the animation should auto start or not. If set to false, you need to start the animation manually.
        /// </summary>
        /// <remarks>
        /// AutoStart for an animation requires to have a helper that looks for the next frame therefore manually starting the Animation will always be more efficient.
        /// </remarks>
        public bool AutoStart { get; set; }

        private int skipFrames;
        /// <summary>
        /// The amount of frames that the animation will skip from the moment it started till the animation begins.
        /// </summary>
        public int SkipFrames
        {
            get => skipFrames;
            set
            {
                StartHelperType |= StartHelperType.SkipFrames;
                skipFrames = value;
            }
        }

        private float delay;
        /// <summary>
        /// The amount of time that the animation will skip from the moment it started till the animation begins.
        /// </summary>
        public float Delay
        {
            get => delay;
            set
            {
                StartHelperType |= StartHelperType.Delay;
                delay = value;
            }
        }

        private Func<bool> delayUntil;
        /// <summary>
        /// The condition that will let the animation begin when it returns true.
        /// </summary>
        public Func<bool> DelayUntil
        {
            get => delayUntil;
            set
            {
                StartHelperType |= StartHelperType.DelayUntil;
                delayUntil = value;
            }
        }

        internal StartHelperType StartHelperType { get; private set; }

        private float timeScale = DefaultTimeScale;

        /// <summary>
        /// The scale of the time that will be applied to the animation.
        /// </summary>
        public float TimeScale
        {
            get => timeScale;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ErrorTimeScaleNegative);
                }

                timeScale = value;
            }
        }

        private int? loopCount = 1;

        /// <summary>
        /// The number of loops the animation will run. Use <b>null</b> for infinite loops.
        /// </summary>
        public int? LoopCount
        {
            get => loopCount;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ErrorLoopCountNegative);
                }

                loopCount = value;
            }
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public AbstractAnimationOptions SetName(string name)
        {
            Name = name;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public AbstractAnimationOptions SetUpdateType(UpdateType updateType)
        {
            UpdateType = updateType;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public AbstractAnimationOptions SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public AbstractAnimationOptions SetSkipFrames(int frames)
        {
            SkipFrames = frames;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public AbstractAnimationOptions SetDelay(float time)
        {
            Delay = time;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelayUntil
        public AbstractAnimationOptions SetDelayUntil(Func<bool> callback)
        {
            DelayUntil = callback;
            return this;
        }
        
        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetWaitFor
        public AbstractAnimationOptions SetWaitFor(params AbstractAnimation[] animations)
        {
            SetDelayUntil(AbstractAnimation.GetWaitForCallback(animations));
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public AbstractAnimationOptions SetLoopCount(int? loopCount)
        {
            LoopCount = loopCount;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public AbstractAnimationOptions SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            return this;
        }
    }
}