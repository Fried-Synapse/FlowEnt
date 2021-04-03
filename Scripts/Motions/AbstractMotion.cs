
namespace FlowEnt
{
    public interface IMotion
    {
        public void OnStart();
        public void OnUpdate(float t);
        public void OnComplete();
    }

    public abstract class AbstractMotion : IMotion
    {
        public abstract void OnStart();
        public abstract void OnUpdate(float t);
        public abstract void OnComplete();
    }

    public abstract class AbstractMotion<T> : IMotion
    {
        public AbstractMotion(T item)
        {
            Item = item;
        }

        public T Item { get; }

        public abstract void OnStart();
        public abstract void OnUpdate(float t);
        public abstract void OnComplete();
    }
}