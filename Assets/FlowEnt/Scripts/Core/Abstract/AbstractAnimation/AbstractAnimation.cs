using System;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides animation specific behaviour
    /// </summary>
    public abstract partial class AbstractAnimation : AbstractUpdatable,
        IFluentControllable<AbstractAnimation>,
        IControllable
    {
        #region Properties       

        private protected AbstractStartHelper startHelper;
        private protected AutoStartHelper autoStartHelper;

        private protected float? overdraft;
        /// <summary>
        /// THe amount of scaled time unconsumed by this animation from the last frame.
        /// </summary>
        public float? OverDraft { get => overdraft; internal set => overdraft = value; }

        #endregion

        #region Controls

        /// <summary>
        /// Starts the animation.
        /// </summary>
        public AbstractAnimation Start()
        {
            PreStart();
            StartInternal();
            return this;
        }

        /// <summary>
        /// Starts the animation async(you can await this till the animation finishes).
        /// </summary>
        public async Task<AbstractAnimation> StartAsync()
        {
            PreStart();
            StartInternal();
            await new AwaitableAnimation(this);
            return this;
        }

        /// <summary>
        /// Resumes the animation.
        /// </summary>
        public AbstractAnimation Resume()
        {
            if (playState != PlayState.Paused)
            {
                return this;
            }
            playState = PlayState.Playing;

            updateController.SubscribeToUpdate(this);
            return this;
        }

        void IControllable.Resume()
            => Resume();

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public AbstractAnimation Pause()
        {
            if (PlayState != PlayState.Playing)
            {
                return this;
            }
            playState = PlayState.Paused;

            updateController.UnsubscribeFromUpdate(this);
            return this;
        }

        void IControllable.Pause()
            => Pause();

        /// <inheritdoc cref="AbstractUpdatable.Stop(bool)"/>
        /// \copydoc AbstractUpdatable.Stop
        //TODO make this return AbstractAnimation.
        public override void Stop(bool triggerOnCompleted = false)
        {
            switch (playState)
            {
                case PlayState.Building:
                case PlayState.Finished:
                    return;
                case PlayState.Waiting:
                    updateController.UnsubscribeFromUpdate(startHelper);
                    break;
                case PlayState.Playing:
                case PlayState.Paused:
                    updateController.UnsubscribeFromUpdate(this);
                    break;
            }

            playState = PlayState.Finished;

            if (triggerOnCompleted)
            {
                onCompleted?.Invoke();
            }
        }

        //TODO this should be implemented by the default stop.
        AbstractAnimation IFluentControllable<AbstractAnimation>.Stop(bool triggerOnCompleted)
        {
            Stop(triggerOnCompleted);
            return this;
        }

        /// <summary>
        /// Resets the animation so in can be replayed.
        /// </summary>
        public AbstractAnimation Reset()
        {
            switch (this)
            {
                case Tween tween:
                    tween.Reset();
                    return this;
                case Echo echo:
                    echo.Reset();
                    return this;
                case Flow flow:
                    flow.Reset();
                    return this;
            }
            return this;
        }

        protected void ResetInternal()
        {
            if (playState != PlayState.Finished)
            {
                throw new AnimationException(this, "Can only reset a finished animation. Use Stop() to ensure animation finished when resetting.");
            }

            startHelper = null;
            autoStartHelper = null;
            playState = PlayState.Building;
            overdraft = null;
        }

        /// <summary>
        /// Provides a task that can be awaited. The task completes when the animation ends.
        /// </summary>
        public async Task AsAsync()
        {
            await new AwaitableAnimation(this);
        }

        #endregion

        #region Lifecycle

        private void OnAutoStarted(float deltaTime)
        {
            if (playState != PlayState.Building)
            {
                return;
            }

            StartInternal(deltaTime);
        }

        internal void CancelAutoStart()
        {
            updateController.UnsubscribeFromUpdate(autoStartHelper);
            autoStartHelper = null;
        }

        private protected void StartSkipFrames()
        {
            //NOTE autostart already skips one frame, so we're skipping it
            if (autoStartHelper != null)
            {
                --skipFrames;
            }
            startHelper = new SkipFramesStartHelper(updateController, updateType, skipFrames, (deltaTime) =>
            {
                skipFrames = 0;
                StartInternal(deltaTime);
            });
        }

        private protected void StartDelay()
        {
            startHelper = new DelayedStartHelper(updateController, updateType, delay, (deltaTime) =>
            {
                delay = -1f;
                StartInternal(deltaTime);
            });
        }

        private void PreStart()
        {
            if (playState != PlayState.Building)
            {
                throw new AnimationException(this, "Animation already started.");
            }

            if (autoStartHelper != null)
            {
                CancelAutoStart();
            }
        }

        internal void OnCompletedInternal(Action callback)
        {
            onCompleted += callback;
        }

        #endregion
    }
}
