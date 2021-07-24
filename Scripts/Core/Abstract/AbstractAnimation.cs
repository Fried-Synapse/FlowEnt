using System;
using System.Threading.Tasks;

namespace FlowEnt
{
    public class AbstractAnimationOptions
    {
        public bool AutoStart { get; set; }

        public AbstractAnimationOptions(bool autoStart = false)
        {
            AutoStart = autoStart;
        }
    }

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

        private protected AutoStartHelper AutoStartHelper { get; set; }
        internal abstract void OnCompletedInternal(Action callback);

        #region Settings Properties

        protected bool IsSubscribedToUpdate { get; set; }

        public PlayState PlayState { get; protected set; } = PlayState.Building;

        #endregion

        #region Lifecycle

        protected abstract void OnAutoStarted(float deltaTime);

        internal void CancelAutoStart()
        {
            FlowEntController.Instance.UnsubscribeFromUpdate(AutoStartHelper);
            AutoStartHelper = null;
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
