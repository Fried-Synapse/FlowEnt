using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    /// <summary>
    /// Lerps an <see cref="ICurve" />.
    /// </summary>
    public class CurveValueMotion : AbstractEventMotion<Vector3>
    {
        [Serializable]
        public class Builder : AbstractEventMotionBuilder, IGizmoDrawer
        {
            [SerializeField]
            protected CurveBuilder curve;

            public override AbstractTweenMotion Build()
                => new CurveValueMotion(curve.Build(), GetCallback());

#if UNITY_EDITOR
            void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
            {
                ((IGizmoDrawer)curve).OnGizmosDrawing(options ?? GizmoOptions);
            }
#endif
        }

        public CurveValueMotion(ICurve curve, Action<Vector3> onUpdated) : base(onUpdated)
        {
            this.curve = curve;
        }

        private readonly ICurve curve;

        public override void OnUpdate(float t)
        {
            onUpdated(curve.GetPoint(t));
        }
    }
}