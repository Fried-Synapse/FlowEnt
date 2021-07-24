using System;
using System.Runtime.CompilerServices;

namespace FlowEnt
{
    internal class AwaitableAnimation
    {
        public AwaitableAnimation(AbstractAnimation animation)
        {
            this.animation = animation;
        }

        private readonly AbstractAnimation animation;
        public AnimationAwaiter GetAwaiter()
            => new AnimationAwaiter(animation);
    }

    internal class AnimationAwaiter : INotifyCompletion
    {
        public AnimationAwaiter(AbstractAnimation animation)
        {
            this.animation = animation;
            this.animation.OnCompletedInternal(() => onCompletedCallback());
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
}
