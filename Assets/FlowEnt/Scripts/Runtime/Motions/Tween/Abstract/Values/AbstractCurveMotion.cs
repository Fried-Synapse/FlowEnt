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
            private protected virtual Transform GizmoTransform => null;
            void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
            {
                if (item == null)
                {
                    return;
                }

                options ??= GizmoOptions;
                if (GizmoTransform != null)
                {
                    options.Transform = GizmoTransform;
                }

                ((IGizmoDrawer)curve).OnGizmosDrawing(options);
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