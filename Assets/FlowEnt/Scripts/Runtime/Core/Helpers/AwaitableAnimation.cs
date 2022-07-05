using System;
using System.Runtime.CompilerServices;

namespace FriedSynapse.FlowEnt
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
        }

        private readonly AbstractAnimation animation;
        public bool IsCompleted => animation.PlayState == PlayState.Finished;

        public AbstractAnimation GetResult()
            => animation;

        public void OnCompleted(Action continuation)
            => animation.OnCompletedInternal(continuation);
    }
}
