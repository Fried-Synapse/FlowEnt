
using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Abstract Motion that can be used to create a simple motion.
    /// </summary>
    public abstract class AbstractMotion : IMotion
    {
        public virtual void OnStart() { }
        public abstract void OnUpdate(float t);
        public virtual void OnLoopComplete() { }
        public virtual void OnComplete() { }
    }

    [Serializable]
    public abstract class AbstractMotionBuilder<T> : IMotionBuilder
    {
        [SerializeField]
        protected T item;
        public T Item => item;

        public abstract IMotion Build();
    }

    /// <summary>
    /// Generic Abstract Motion
    /// </summary>
    /// <typeparam name="T">Generic Type for the motion. There is a read only property of type <T> called item that can be used and it's required on the constructor.</typeparam>
    public abstract class AbstractMotion<T> : AbstractMotion
    {
        protected AbstractMotion(T item)
        {
            this.item = item;
        }

        protected readonly T item;
        public T Item => item;
    }
}