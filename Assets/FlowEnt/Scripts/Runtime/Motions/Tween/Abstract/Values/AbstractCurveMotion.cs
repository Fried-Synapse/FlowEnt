using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public abstract class AbstractCurveMotion<TItem> : AbstractTweenMotion<TItem>
    {
        [Serializable]
        public new abstract class AbstractBuilder : AbstractTweenMotion<TItem>.AbstractBuilder, IGizmoDrawer
        {
            [SerializeField]
            protected CurveBuilder curve;

#if UNITY_EDITOR
            void IGizmoDrawer.OnGizmosDrawing()
            {
                ((IGizmoDrawer)curve).OnGizmosDrawing();
            }
#endif
        }

        protected AbstractCurveMotion(TItem item, ICurve curve) : base(item)
        {
            this.curve = curve;
        }

        protected readonly ICurve curve;
    }
}