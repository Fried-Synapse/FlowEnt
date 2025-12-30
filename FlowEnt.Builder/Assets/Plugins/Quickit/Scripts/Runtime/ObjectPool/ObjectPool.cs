using System;

namespace FriedSynapse.Quickit
{
    public class ObjectPool<T> : AbstractObjectPool<T>
        where T : class, new()
    {
        public ObjectPool(int startAmount, Action<T> onObjectAdded = null) : base(onObjectAdded)
        {
            Increase(startAmount);
        }

        protected override T CreateObject()
        {
            return new T();
        }
    }
}