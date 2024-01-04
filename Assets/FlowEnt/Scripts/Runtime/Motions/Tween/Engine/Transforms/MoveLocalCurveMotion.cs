using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value using a spline.
    /// </summary>
    public class MoveLocalCurveMotion : AbstractCurveMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractBuilder, IGizmoDrawer
        {
            public override AbstractTweenMotion Build()
                => new MoveLocalCurveMotion(item, curve.Build());

#if UNITY_EDITOR
            void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
            {
                options ??= GizmoOptions;
                if (item != null)
                {
                    options.PositionOffset = item.position;
                }

                ((IGizmoDrawer)curve).OnGizmosDrawing(options ?? GizmoOptions);
            }
#endif
        }

        public MoveLocalCurveMotion(Transform item, ICurve curve) : base(item, curve)
        {
        }

        public override void OnUpdate(float t)
        {
            item.localPosition = curve.GetPoint(t);
        }
    }
}