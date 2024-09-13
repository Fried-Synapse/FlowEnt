using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value.
    /// </summary>
    public class MoveLocalVectorMotion : AbstractVector3MotionWithGizmo<Transform>
    {
        [Serializable]
        public class ValueBuilder : AbstractVector3ValueBuilderWithGizmo
        {
            public override AbstractTweenMotion Build()
                => new MoveLocalVectorMotion(item, value);

#if UNITY_EDITOR
            private protected override (Vector3 Start, Vector3 End) GizmoLine 
                => (item.position, item.TransformPoint(value));
#endif
        }

        [Serializable]
        public class FromToBuilder : AbstractVector3FromToBuilderWithGizmo
        {
            public override AbstractTweenMotion Build()
                => new MoveLocalVectorMotion(item, From, to);

#if UNITY_EDITOR
            private protected override (Vector3 Start, Vector3 End) GizmoLine 
                => (From != null ? item.TransformPoint(From.Value) : item.position, item.TransformPoint(to));
#endif
        }

        public MoveLocalVectorMotion(Transform item, Vector3 value) : base(item, value)
        {
        }

        public MoveLocalVectorMotion(Transform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.localPosition;
        protected override void SetValue(Vector3 value) => item.localPosition = value;
    }
}