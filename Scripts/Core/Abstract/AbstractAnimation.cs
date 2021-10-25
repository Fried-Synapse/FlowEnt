using System;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides animation specific behaviour
    /// </summary>
    public abstract class AbstractAnimation : AbstractUpdatable
    {
        private protected AbstractStartHelper startHelper;
        private protected AutoStartHelper autoStartHelper;
        internal bool HasAutoStart => autoStartHelper != null;

        #region Options

        private protected int skipFrames;
        private protected float delay = -1f;
        private protected int? loopCount = 1;
        private protected float timeScale = 1f;

        #endregion

        #region Events

        private protected Action<int?> onLoopCompleted;

        #endregion

        #region Settings Properties

        protected bool AutoStart
        {
            set
            {
                if (value)
                {
                    autoStartHelper = new AutoStartHelper(updateController, OnAutoStarted);
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
        private protected PlayState playState = PlayState.Building;
        /// <summary>
        /// The current state of the animation.
        /// </summary>
        public PlayState PlayState => playState;
        private protected float? overdraft;
        /// <summary>
        /// THe amount of scaled time unconsumed by this animation from the last frame.
        /// </summary>
        public float? OverDraft { get => overdraft; internal set => overdraft = value; }

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
            startHelper = new SkipFramesStartHelper(updateController, skipFrames, (deltaTime) =>
            {
                skipFrames = 0;
                StartInternal(deltaTime);
            });
        }

        private protected void StartDelay()
        {
            startHelper = new DelayedStartHelper(updateController, delay, (deltaTime) =>
            {
                delay = -1f;
                StartInternal(deltaTime);
            });
        }

        /// <summary>
        /// Resumes the animation.
        /// </summary>
        public void Resume()
        {
            if (playState != PlayState.Paused)
            {
                return;
            }
            playState = PlayState.Playing;

            updateController.SubscribeToUpdate(this);
        }

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public void Pause()
        {
            if (PlayState != PlayState.Playing)
            {
                return;
            }
            playState = PlayState.Paused;

            updateController.UnsubscribeFromUpdate(this);
        }

        /// <inheritdoc />
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

        /// <summary>
        /// Provides a task that can be awaited. The task completes when the animation ends.
        /// </summary>
        public async Task AsAsync()
        {
            await new AwaitableAnimation(this);
        }

        #endregion

        internal void OnCompletedInternal(Action callback)
        {
            onCompleted += callback;
        }
    }
}
