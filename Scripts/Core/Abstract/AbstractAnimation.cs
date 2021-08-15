using System;
using System.Threading.Tasks;

namespace FlowEnt
{
    public abstract class AbstractAnimation : AbstractUpdatable
    {
        protected AbstractAnimation(bool autoStart = false)
        {
            if (autoStart)
            {
                autoStartHelper = new AutoStartHelper(updateController, OnAutoStarted);
            }
        }

        private protected AutoStartHelper autoStartHelper;
        internal bool HasAutoStart => autoStartHelper != null;
        internal abstract void OnCompletedInternal(Action callback);

        #region Options

        private protected int skipFrames;
        private protected float delay = -1f;
        private protected int? loopCount = 1;
        private protected float timeScale = 1f;

        #endregion

        #region Events
        private protected Action onStarted;
        private protected Action onCompleted;

        #endregion

        #region Settings Properties

        private protected PlayState playState;
        public PlayState PlayState => playState;
        private protected float? overdraft;
        public float? OverDraft => overdraft;

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
            SkipFramesStartHelper skipFramesStartHelper = new SkipFramesStartHelper(updateController, skipFrames, (deltaTime) =>
            {
                skipFrames = 0;
                StartInternal(deltaTime);
            });
        }

        private protected void StartDelay()
        {
            DelayedStartHelper delayedStartHelper = new DelayedStartHelper(updateController, delay, (deltaTime) =>
            {
                delay = -1f;
                StartInternal(deltaTime);
            });
        }

        internal abstract void StartInternal(float deltaTime = 0);

        public void Resume()
        {
            if (playState != PlayState.Paused)
            {
                return;
            }
            playState = PlayState.Playing;

            updateController.SubscribeToUpdate(this);
        }

        public void Pause()
        {
            if (PlayState != PlayState.Playing)
            {
                return;
            }
            playState = PlayState.Paused;

            updateController.UnsubscribeFromUpdate(this);
        }

        public void Stop(bool triggerOnCompleted = false)
        {
            if (!(playState == PlayState.Playing || playState == PlayState.Paused))
            {
                return;
            }
            playState = PlayState.Finished;

            updateController.UnsubscribeFromUpdate(this);

            if (triggerOnCompleted)
            {
                onCompleted?.Invoke();
            }
        }

        public async Task AsAsync()
        {
            await new AwaitableAnimation(this);
        }

        #endregion

    }
}
