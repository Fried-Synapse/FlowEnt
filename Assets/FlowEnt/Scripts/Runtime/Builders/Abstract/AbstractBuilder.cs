namespace FriedSynapse.FlowEnt
{
    public interface IBuilder<out TItem>
    {
        public TItem Build();
    }

    public abstract class AbstractBuilder
    {
    }

    public abstract class AbstractBuilder<TItem> : AbstractBuilder, IBuilder<TItem>
    {
        public abstract TItem Build();
    }
}