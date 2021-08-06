
namespace FlowEnt
{
    /// <summary>
    /// Abstract Motion that can be used to create a simple motion.
    /// </summary>
    public abstract class AbstractMotion : IMotion
    {
        public virtual void OnStart() { }
        public abstract void OnUpdate(float t);
        public virtual void OnComplete() { }
    }

    /// <summary>
    /// Generic Abstract Motion
    /// </summary>
    /// <typeparam name="T">Generic Type for the motion. There is a read only property of type <T> called item that can be used and it's required on the constructor.</typeparam>
    public abstract class AbstractMotion<T> : IMotion
    {
        protected AbstractMotion(T item)
        {
            this.item = item;
        }

        protected readonly T item;
        public T Item => item;

        public virtual void OnStart() { }
        public abstract void OnUpdate(float t);
        public virtual void OnComplete() { }
    }
}