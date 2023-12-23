using System;
using FriedSynapse.FlowEnt.Motions.Abstract;

namespace FriedSynapse.FlowEnt.Motions.Echo.Abstract
{
    /// <summary>
    /// Abstract Motion that can be used to create a simple motion.
    /// </summary>
    public abstract class AbstractEchoMotion : AbstractMotion, IEchoMotion
    {
        public virtual void OnStart() { }
        public abstract void OnUpdate(float deltaTime);
        public virtual void OnLoopStart() { }
        public virtual void OnLoopComplete() { }
        public virtual void OnComplete() { }
    }

    /// <summary>
    /// Generic Abstract Motion
    /// </summary>
    /// <typeparam name="TItem">Generic Type for the motion. There is a read only property of type <T> called item that can be used and it's required on the constructor.</typeparam>
    public abstract class AbstractEchoMotion<TItem> : AbstractEchoMotion
    {
        [Serializable]
        public abstract class AbstractBuilder : AbstractEchoMotionBuilder<TItem>
        {
        }

        protected AbstractEchoMotion(TItem item)
        {
            this.item = item;
        }

        protected readonly TItem item;
        public TItem Item => item;
    }
}