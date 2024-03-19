using System.Threading;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides animation specific behaviour
    /// </summary>
    public abstract partial class AbstractAnimation : AbstractUpdatable, IFluentControllable<AbstractAnimation>
    {
        /// <inheritdoc cref="AbstractUpdatable()"/>
        protected AbstractAnimation()
        {
        }

        #region Members

        private protected float elapsedTime;
        private protected AbstractStartHelper startHelper;
        private protected AutoStartHelper autoStartHelper;

        private protected float? overdraft;

        /// <summary>
        /// THe amount of scaled time unconsumed by this animation from the last frame.
        /// </summary>
        public float? Overdraft { get => overdraft; internal set => overdraft = value; }

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
        /// <param name="token">The cancellation doesn't cancel the task, but rather terminates the animation. The task will be completed</param>
        public async Task<AbstractAnimation> StartAsync(CancellationToken? token = null)
        {
            AnimationAwaiter awaiter = new AnimationAwaiter(this);
            token?.Register(() =>
            {
                Stop();
                awaiter.Complete();
            });
            PreStart();
            StartInternal();

            await awaiter;
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

            if (startHelper != null)
            {
                startHelper.Resume();
            }
            else
            {
                updateController.SubscribeToUpdate(this);
            }

            return this;
        }

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public AbstractAnimation Pause()
        {
            switch (PlayState)
            {
                case PlayState.Waiting:
                    playState = PlayState.Paused;
                    startHelper.Pause();
                    break;
                case PlayState.Playing:
                    playState = PlayState.Paused;
                    updateController.UnsubscribeFromUpdate(this);
                    break;
            }

            return this;
        }

        /// <inheritdoc cref="AbstractUpdatable.Stop(bool)"/>
        /// \copydoc AbstractUpdatable.Stop
        public new AbstractAnimation Stop(bool triggerOnCompleted = false)
        {
            StopInternal(triggerOnCompleted);
            return this;
        }

        /// <summary>
        /// Resets the animation so in can be replayed.
        /// </summary>
        /// <exception cref="AnimationException">If the animation is not finished.</exception>
        public new AbstractAnimation Reset()
        {
            ResetInternal();
            return this;
        }

        /// <summary>
        /// Provides a task that can be awaited. The task completes when the animation ends.
        /// </summary>
        public async Task AsAsync(CancellationToken? token = null)
        {
            AnimationAwaiter awaiter = new(this);
            token?.Register(() =>
            {
                Stop();
                awaiter.Complete();
            });
            await awaiter;
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

        private protected bool TryStartHelpers()
        {
            if ((startHelperType & StartHelperType.SkipFrames) != StartHelperType.None && skipFrames > 0)
            {
                StartSkipFrames();
                return true;
            }

            if ((startHelperType & StartHelperType.Delay) != StartHelperType.None && delay > 0)
            {
                StartDelay();
                return true;
            }

            if ((startHelperType & StartHelperType.DelayUntil) != StartHelperType.None && delayUntil != null)
            {
                StartDelayUntil();
                return true;
            }

            return false;
        }

        private void StartSkipFrames()
        {
            playState = PlayState.Waiting;
            //NOTE autostart already skips one frame, so we're skipping it
            if (autoStartHelper != null)
            {
                --skipFrames;
            }

            startHelper = new SkipFramesStartHelper(updateController, updateType, skipFrames, (deltaTime) =>
            {
                startHelper = null;
                startHelperType &= ~StartHelperType.SkipFrames;
                StartInternal(deltaTime);
            });
        }

        private void StartDelay()
        {
            playState = PlayState.Waiting;
            startHelper = new DelayStartHelper(updateController, updateType, delay, (deltaTime) =>
            {
                startHelper = null;
                startHelperType &= ~StartHelperType.Delay;
                StartInternal(deltaTime);
            });
        }

        private void StartDelayUntil()
        {
            playState = PlayState.Waiting;
            startHelper = new DelayUntilStartHelper(updateController, updateType, delayUntil, (deltaTime) =>
            {
                startHelper = null;
                startHelperType &= ~StartHelperType.DelayUntil;
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

        protected override void StopInternal(bool triggerOnCompleted)
        {
            base.StopInternal(triggerOnCompleted);
            switch (playState)
            {
                case PlayState.Building:
                    break;
                case PlayState.Waiting:
                    if (startHelper != null)
                    {
                        updateController.UnsubscribeFromUpdate(startHelper);
                    }

                    break;
                case PlayState.Playing:
                    updateController.UnsubscribeFromUpdate(this);
                    break;
                case PlayState.Paused:
                    updateController.UnsubscribeFromUpdate((AbstractUpdatable)startHelper ?? this);
                    break;
                case PlayState.Finished:
                    return;
            }

            playState = PlayState.Finished;

            if (triggerOnCompleted)
            {
                onCompleted?.Invoke();
            }
        }

        protected override void ResetInternal()
        {
            if (playState != PlayState.Finished)
            {
                throw new AnimationException(this, "Can only reset a finished animation. Use Stop() to ensure animation finished when resetting.");
            }

            base.ResetInternal();
            elapsedTime = 0f;
            startHelper = null;
            autoStartHelper = null;
            playState = PlayState.Building;
            overdraft = null;
        }

        #endregion
    }
}