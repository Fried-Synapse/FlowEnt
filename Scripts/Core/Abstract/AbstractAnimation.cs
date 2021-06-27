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
                Animation = animation;
            }

            public AbstractAnimation Animation { get; }
            public AnimationAwaiter GetAwaiter()
                => new AnimationAwaiter(Animation);
        }

        protected class AnimationAwaiter : INotifyCompletion
        {
            public AnimationAwaiter(AbstractAnimation animation)
            {
                Animation = animation;
                Animation.OnCompleteCallback += () => OnCompletedCallback.Invoke();
            }

            public AbstractAnimation Animation { get; }
            public bool IsCompleted => Animation.PlayState == PlayState.Finished;
            private Action OnCompletedCallback { get; set; }

            public AbstractAnimation GetResult()
                => Animation;

            public void OnCompleted(Action continuation)
            {
                OnCompletedCallback = continuation;
            }
        }

        public AbstractAnimation(bool autoStart = false)
        {
            if (autoStart)
            {
                FlowEntController.Instance.SubscribeToUpdate(new AutoStartHelper(OnAutoStart));
            }
        }

        protected Action OnStartCallback { get; set; }
        protected Action OnCompleteCallback { get; set; }

        #region Settings Properties

        protected bool IsSubscribedToUpdate { get; set; }

        public PlayState PlayState { get; protected set; } = PlayState.Building;

        #endregion

        #region Flow Data

        public Tween Next { get; protected set; }

        #endregion

        #region Lifecycle

        protected abstract void OnAutoStart(float deltaTime);

        internal abstract void StartInternal(bool subscribeToUpdate = true);

        public void Pause()
        {
            if (!IsSubscribedToUpdate)
            {
                return;
            }
            FlowEntController.Instance.UnsubscribeFromUpdate(this);
        }

        public void Play()
        {
            if (!IsSubscribedToUpdate)
            {
                return;
            }
            FlowEntController.Instance.SubscribeToUpdate(this);
        }

        public async Task AsAsync()
        {
            await new AwaitableAnimation(this);
        }

        #endregion

        #region Setters

        public void OnComplete(Action callback)
        {
            OnCompleteCallback += callback;
        }

        #endregion
    }
}
