using System;
using System.Runtime.CompilerServices;

namespace FriedSynapse.FlowEnt
{
    internal class AnimationAwaiter : INotifyCompletion
    {
        public AnimationAwaiter(AbstractAnimation animation)
        {
            this.animation = animation;
        }

        private readonly AbstractAnimation animation;
        public bool IsCompleted => animation.PlayState == PlayState.Finished;
        private Action continuation;

        public AbstractAnimation GetResult()
            => animation;

        public void OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            animation.OnCompletedInternal(continuation);
        }

        public void Complete()
        {
            this.continuation?.Invoke();
        }

        public AnimationAwaiter GetAwaiter()
            => this;
    }
}
