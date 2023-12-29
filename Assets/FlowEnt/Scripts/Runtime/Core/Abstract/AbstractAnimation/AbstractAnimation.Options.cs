using System;

namespace FriedSynapse.FlowEnt
{
    public partial class AbstractAnimation : IFluentAnimationOptionable<AbstractAnimation>
    {
        [Flags]
        private protected enum StartHelperEnum
        {
            None = 0,
            SkipFrames = 1 << 0,
            Delay = 1 << 1,
            DelayUntil = 1 << 2,
        }

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

        private protected StartHelperEnum startHelperType;
        private int skipFrames;
        private float delay;
        private Func<bool> delayUntilCondition;
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
            startHelperType |= skipFrames != options.SkipFrames ? StartHelperEnum.SkipFrames : StartHelperEnum.None;
            skipFrames = options.SkipFrames;
            startHelperType |= delay != options.Delay ? StartHelperEnum.Delay : StartHelperEnum.None;
            delay = options.Delay;
            startHelperType |= delayUntilCondition != options.DelayUntil ? StartHelperEnum.DelayUntil : StartHelperEnum.None;
            delayUntilCondition = options.DelayUntil;
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
            startHelperType |= StartHelperEnum.SkipFrames;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelay
        public AbstractAnimation SetDelay(float time)
        {
            delay = time;
            startHelperType |= StartHelperEnum.Delay;
            return this;
        }

        /// <inheritdoc />
        /// \copydoc IFluentAnimationOptionable.SetDelayUntil
        public AbstractAnimation SetDelayUntil(Func<bool> callback)
        {
            delayUntilCondition = callback;
            startHelperType |= StartHelperEnum.DelayUntil;
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