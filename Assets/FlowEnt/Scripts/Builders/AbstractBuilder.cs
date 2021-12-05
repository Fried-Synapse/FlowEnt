namespace FriedSynapse.FlowEnt
{
    public interface IBuilder<T>
    {
        public T Build();
    }

    public abstract class AbstractBuilder<T> : IBuilder<T>
    {
        public abstract T Build();
    }
}
