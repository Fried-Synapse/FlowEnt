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

        void INotifyCompletion.OnCompleted(Action continuation)
        {
            this.continuation = continuation;
            animation.OnCompleted(continuation);
        }

        public void Complete()
        {
            continuation?.Invoke();
        }

        public AnimationAwaiter GetAwaiter()
            => this;

        public AbstractAnimation GetResult()
            => animation;
    }
}
