namespace FriedSynapse.FlowEnt
{
    public interface IAbstractAnimationBuilder : IListBuilderItem
    {
        public AbstractAnimation Build();
    }

    public abstract class AbstractAnimationBuilder<TAnimation> : AbstractBuilder<TAnimation>, IAbstractAnimationBuilder
        where TAnimation : AbstractAnimation
    {
        AbstractAnimation IAbstractAnimationBuilder.Build()
            => Build();
    }
}