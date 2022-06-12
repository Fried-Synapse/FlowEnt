using System;

namespace FriedSynapse.FlowEnt
{
    public partial class AbstractAnimation : IFluentAnimationOptionable<AbstractAnimation>
    {
        public UpdateType UpdateType
        {
            get => updateType;
            set
            {
                updateType = value;
                if (AutoStart)
                {
                    autoStartHelper.updateType = updateType;
                }
            }
        }

        public bool AutoStart
        {
            get => autoStartHelper != null;
            protected set
            {
                if (value)
                {
                    autoStartHelper = new AutoStartHelper(updateController, updateType, OnAutoStarted);
                    startHelper = autoStartHelper;
                }
                else
                {
                    if (autoStartHelper != null)
                    {
                        CancelAutoStart();
                    }
                }
            }
        }
        private protected int skipFrames;
        private protected float delay;
        private protected int? loopCount = AbstractAnimationOptions.DefaultLoopCount;
        private protected float timeScale = AbstractAnimationOptions.DefaultTimeScale;
        public float TimeScale
        {
            get => timeScale;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(AbstractAnimationOptions.ErrorTimeScaleNegative);
                }
                timeScale = value;
            }
        }

        private protected PlayState playState = PlayState.Building;
        /// <summary>
        /// The current state of the animation.
        /// </summary>
        public PlayState PlayState => playState;

        /// <summary>
        /// Executes the <paramref name="onConditionTrue"/> if <paramref name="condition"/> returns true.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="onConditionTrue">The callback.</param>
        protected TAnimation ConditionalInternal<TAnimation>(Func<bool> condition, Action<TAnimation> onConditionTrue)
            where TAnimation : AbstractAnimation
        {
            TAnimation animation = (TAnimation)this;
            if (condition?.Invoke() == true)
            {
                onConditionTrue?.Invoke(animation);
            }

            return animation;
        }

        /// <inheritdoc cref="ConditionalInternal{TAnimation}(Func{bool}, Action{TAnimation})" />
        /// \copydoc ConditionalInternal{TAnimation}(Func{bool}, Action{TAnimation})
        public AbstractAnimation Conditional(Func<bool> condition, Action<AbstractAnimation> onConditionTrue)
            => ConditionalInternal(condition, onConditionTrue);

        protected void SetOptions(AbstractAnimationOptions options)
        {
            Name = options.Name;
            updateType = options.UpdateType;
            AutoStart = options.AutoStart;
            skipFrames = options.SkipFrames;
            delay = options.Delay;
            timeScale = options.TimeScale;
            loopCount = options.LoopCount;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetName
        public AbstractAnimation SetName(string name)
        {
            Name = name;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetUpdateType
        public AbstractAnimation SetUpdateType(UpdateType updateType)
        {
            UpdateType = updateType;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetAutoStart
        public AbstractAnimation SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetSkipFrames
        public AbstractAnimation SetSkipFrames(int frames)
        {
            skipFrames = frames;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public AbstractAnimation SetDelay(float time)
        {
            delay = time;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetLoopCount
        public AbstractAnimation SetLoopCount(int? loopCount)
        {
            if (loopCount <= 0)
            {
                throw new ArgumentException(AbstractAnimationOptions.ErrorLoopCountNegative);
            }
            this.loopCount = loopCount;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetTimeScale
        public AbstractAnimation SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            return this;
        }
    }
}
