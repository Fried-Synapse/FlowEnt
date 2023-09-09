using System;

namespace FriedSynapse.FlowEnt
{
    public interface IAbstractAnimationBuilder : IBuilderListItem
    {
        public AbstractAnimation Build();
    }

    public abstract class AbstractAnimationBuilder<TAnimation> : IAbstractAnimationBuilder
        where TAnimation : AbstractAnimation
    {
        public abstract TAnimation Build();

        AbstractAnimation IAbstractAnimationBuilder.Build()
            => Build();
    }
}