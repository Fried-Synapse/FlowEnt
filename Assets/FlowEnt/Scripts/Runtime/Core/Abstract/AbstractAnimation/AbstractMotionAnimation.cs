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
        private protected readonly List<TMotion> motions = new(1);
        private readonly List<Object> objects = new(1);

        #region Motions

        /// <summary>
        /// Applies all the motions to the current tween.
        /// </summary>
        /// <param name="motions"></param>
        public AbstractMotionAnimation<TMotion> Apply(params TMotion[] motions)
        {
            RegisterObjects(motions.OfType<IObjectMotion>());
            this.motions.AddRange(motions);
            return this;
        }

        /// <inheritdoc cref="Apply(TMotion[])"/>
        /// \copydoc AbstractMotionAnimation.Apply
        /// <param name="motions"></param>
        public AbstractMotionAnimation<TMotion> Apply(IEnumerable<TMotion> motions)
        {
            RegisterObjects(motions.OfType<IObjectMotion>());
            this.motions.AddRange(motions);
            return this;
        }
        
        #endregion

        #region AutoCancel

        private protected override bool ShouldCancel()
        {
            foreach (Object obj in objects)
            {
                if (obj == null)
                {
                    return true;
                }
            }

            return false;
        }

        private protected void RegisterObjects(IEnumerable<IObjectMotion> motions)
        {
            foreach (IObjectMotion motion in motions)
            {
                Object obj = motion.Object;
                if (obj != null)
                {
                    objects.Add(obj);
                }
            }
        }

        #endregion
    }
}