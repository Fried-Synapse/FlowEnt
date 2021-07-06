using System;
using System.Runtime.CompilerServices;
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
        private class AutoStartHelper : AbstractUpdatable
        {
            public AutoStartHelper(Action<float> callback)
            {
                Callback = callback;
            }

            private Action<float> Callback { get; set; }

            internal override float? UpdateInternal(float deltaTime)
            {
                FlowEntController.Instance.UnsubscribeFromUpdate(this);
                Callback.Invoke(deltaTime);
                return null;
            }
        }

        protected class AwaitableAnimation
        {
            public AwaitableAnimation(AbstractAnimation animation)
            {
                this.animation = animation;
            }

            private readonly AbstractAnimation animation;
            public AnimationAwaiter GetAwaiter()
                => new AnimationAwaiter(animation);
        }

        protected class AnimationAwaiter : INotifyCompletion
        {
            public AnimationAwaiter(AbstractAnimation animation)
            {
                this.animation = animation;
                this.animation.OnCompleteInternal(() => onCompletedCallback());
            }

            private readonly AbstractAnimation animation;
            public bool IsCompleted => animation.PlayState == PlayState.Finished;
            private Action onCompletedCallback;

            public AbstractAnimation GetResult()
                => animation;

            public void OnCompleted(Action continuation)
            {
                onCompletedCallback = continuation;
            }
        }

        protected AbstractAnimation(bool autoStart = false)
        {
            if (autoStart)
            {
                FlowEntController.Instance.SubscribeToUpdate(new AutoStartHelper(OnAutoStart));
            }
        }

        protected abstract void OnCompleteInternal(Action callback);

        #region Settings Properties

        protected bool IsSubscribedToUpdate { get; set; }

        public PlayState PlayState { get; protected set; } = PlayState.Building;

        #endregion

        #region Lifecycle

        protected abstract void OnAutoStart(float deltaTime);

        internal abstract void StartInternal(bool subscribeToUpdate = true);

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
