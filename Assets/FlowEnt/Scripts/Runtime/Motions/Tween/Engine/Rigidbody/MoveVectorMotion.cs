using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.position" /> value.
    /// </summary>
    public class MoveVectorMotion : AbstractVector3MotionWithGizmo<Rigidbody>
    {
        [Serializable]
        public class ValueBuilder : AbstractVector3ValueBuilderWithGizmo
        {
            public override AbstractTweenMotion Build()
                => new MoveVectorMotion(item, value);

#if UNITY_EDITOR
            private protected override (Vector3 Start, Vector3 End) GizmoLine
                => (item.position, item.position + value);
#endif
        }

        [Serializable]
        public class FromToBuilder : AbstractVector3FromToBuilderWithGizmo
        {
            public override AbstractTweenMotion Build()
                => new MoveVectorMotion(item, From, to);

#if UNITY_EDITOR
            private protected override (Vector3 Start, Vector3 End) GizmoLine
                => (From ?? item.position, to);
#endif
        }

        public MoveVectorMotion(Rigidbody item, Vector3 value) : base(item, value)
        {
        }

        public MoveVectorMotion(Rigidbody item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.position;
        protected override void SetValue(Vector3 value) => item.position = value;
    }
}