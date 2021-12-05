using System;

namespace FriedSynapse.FlowEnt
{
    public partial class AbstractAnimation : IFluentAnimationEventable<AbstractAnimation>
    {
        /// <inheritdoc />
        public AbstractAnimation OnStarted(Action callback)
        {
            onStarted += callback;
            return this;
        }

        /// <inheritdoc />
        public AbstractAnimation OnUpdated(Action<float> callback)
        {
            onUpdated += callback;
            return this;
        }

        /// <inheritdoc />
        public AbstractAnimation OnLoopCompleted(Action<int?> callback)
        {
            onLoopCompleted += callback;
            return this;
        }

        /// <inheritdoc />
        public AbstractAnimation OnCompleted(Action callback)
        {
            onCompleted += callback;
            return this;
        }
    }
}
