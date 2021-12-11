
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
    public class SerializableMotionBuilder
    {
        [SerializeField]
        protected int xx;
    }

    [Serializable]
    public abstract class AbstractMotionBuilder : SerializableMotionBuilder, IBuilder<IMotion>
    {
        public abstract IMotion Build();
    }

    [Serializable]
    public abstract class AbstractMotionBuilder<T> : AbstractMotionBuilder
    {
        [SerializeField]
        protected T item;
        public T Item => item;
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