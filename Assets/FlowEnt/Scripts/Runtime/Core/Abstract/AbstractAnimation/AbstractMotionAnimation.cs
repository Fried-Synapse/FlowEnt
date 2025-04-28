using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Motions.Abstract;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public abstract class AbstractMotionAnimation<TMotion> : AbstractAnimation
        where TMotion : IMotion
    {
        private protected readonly FastList<TMotion> motions = new(1);
#if FlowEnt_AutoCancel
        private readonly FastList<Object> objects = new(1);
#endif

        #region Motions

        /// <summary>
        /// Applies the motion to the current animation.
        /// </summary>
        /// <param name="motion"></param>
        public AbstractMotionAnimation<TMotion> Apply(TMotion motion)
        {
#if FlowEnt_AutoCancel
            if (motion is IObjectMotion objectMotion)
            {
                RegisterObject(objectMotion);
            }
#endif

            motions.Add(motion);
            return this;
        }

        /// <inheritdoc cref="Apply(TMotion[])"/>
        /// \copydoc AbstractMotionAnimation.Apply
        /// <param name="motions"></param>
        public AbstractMotionAnimation<TMotion> Apply(IEnumerable<TMotion> motions)
        {
#if FlowEnt_AutoCancel
            RegisterObjects(motions.OfType<IObjectMotion>());
#endif
            this.motions.AddRange(motions);
            return this;
        }

        #endregion

        #region AutoCancel

        private protected override bool ShouldCancel()
        {
#if FlowEnt_AutoCancel
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] == null)
                {
                    return true;
                }
            }
#endif

            return false;
        }

#if FlowEnt_AutoCancel
        private void RegisterObject(IObjectMotion motion)
        {
            Object obj = motion.Object;
            if (obj != null)
            {
                objects.Add(obj);
            }
        }

        private void RegisterObjects(IEnumerable<IObjectMotion> motions)
        {
            foreach (IObjectMotion motion in motions)
            {
                RegisterObject(motion);
            }
        }

#endif

        #endregion
    }
}