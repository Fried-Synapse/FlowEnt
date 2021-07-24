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
                AutoStartHelper = new AutoStartHelper(OnAutoStarted);
                FlowEntController.Instance.SubscribeToUpdate(AutoStartHelper);
            }
        }

        internal AutoStartHelper AutoStartHelper { get; private set; }
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

        protected bool IsSubscribedToUpdate { get; set; }

        public PlayState PlayState { get; protected set; }

        #endregion

        #region Lifecycle

        private void OnAutoStarted(float deltaTime)
        {
            if (PlayState != PlayState.Building)
            {
                return;
            }

            StartInternal(true, deltaTime);
        }

        internal void CancelAutoStart()
        {
            FlowEntController.Instance.UnsubscribeFromUpdate(AutoStartHelper);
            AutoStartHelper = null;
        }

        protected void StartSkipFrames(bool subscribeToUpdate)
        {
            //NOTE autostart already skips one frame, so we're skipping it
            if (AutoStartHelper != null)
            {
                --skipFrames;
            }
            SkipFramesStartHelper skipFramesStartHelper = new SkipFramesStartHelper(skipFrames, (deltaTime) =>
            {
                skipFrames = 0;
                StartInternal(subscribeToUpdate, deltaTime);
            });
            FlowEntController.Instance.SubscribeToUpdate(skipFramesStartHelper);
        }

        protected void StartDelay(bool subscribeToUpdate)
        {
            DelayedStartHelper delayedStartHelper = new DelayedStartHelper(delay, (deltaTime) =>
            {
                delay = -1f;
                StartInternal(subscribeToUpdate, deltaTime);
            });
            FlowEntController.Instance.SubscribeToUpdate(delayedStartHelper);
        }

        internal abstract void StartInternal(bool subscribeToUpdate = true, float? deltaTime = null);

        public void Resume()
        {
            if (PlayState != PlayState.Paused)
            {
                return;
            }
            PlayState = PlayState.Playing;
            if (!IsSubscribedToUpdate)
            {
                return;
            }
            FlowEntController.Instance.SubscribeToUpdate(this);
        }

        public void Pause()
        {
            if (PlayState != PlayState.Playing)
            {
                return;
            }
            PlayState = PlayState.Paused;
            if (!IsSubscribedToUpdate)
            {
                return;
            }
            FlowEntController.Instance.UnsubscribeFromUpdate(this);
        }

        public void Stop()
        {
            if (PlayState == PlayState.Finished)
            {
                return;
            }
            PlayState = PlayState.Finished;
            if (!IsSubscribedToUpdate)
            {
                return;
            }
            FlowEntController.Instance.UnsubscribeFromUpdate(this);
        }

        public async Task AsAsync()
        {
            await new AwaitableAnimation(this);
        }

        #endregion

    }
}
