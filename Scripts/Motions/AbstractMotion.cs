
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
        public virtual void OnStart() { }
        public abstract void OnUpdate(float t);
        public virtual void OnComplete() { }
    }

    public abstract class AbstractMotion<T> : IMotion
    {
        protected AbstractMotion(T item)
        {
            Item = item;
        }

        public T Item { get; }

        public virtual void OnStart() { }
        public abstract void OnUpdate(float t);
        public virtual void OnComplete() { }
    }
}