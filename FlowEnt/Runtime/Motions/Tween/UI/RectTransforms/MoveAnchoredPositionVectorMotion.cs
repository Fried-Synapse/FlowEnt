using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms
{
    /// <summary>
    /// Lerps the <see cref="RectTransform.anchoredPosition" /> value.
    /// </summary>
    public class MoveAnchoredPositionVectorMotion : AbstractVector3MotionWithGizmo<RectTransform>
    {
        [Serializable]
        public class ValueBuilder : AbstractVector3ValueBuilderWithGizmo
        {
            public override AbstractTweenMotion Build()
                => new MoveAnchoredPositionVectorMotion(item, value);
            
#if UNITY_EDITOR
            private protected override (Vector3 Start, Vector3 End) GizmoLine 
                => (item.position, item.TransformPoint(value));
#endif
        }

        [Serializable]
        public class FromToBuilder : AbstractVector3FromToBuilderWithGizmo
        {
            public override AbstractTweenMotion Build()
                => new MoveAnchoredPositionVectorMotion(item, From, to);
            
#if UNITY_EDITOR
            private protected override (Vector3 Start, Vector3 End) GizmoLine 
                => (From != null ? item.TransformPoint(From.Value) : item.position, item.TransformPoint(to));
#endif
        }
        
        public MoveAnchoredPositionVectorMotion(RectTransform item, Vector3 value) : base(item, value)
        {
        }

        public MoveAnchoredPositionVectorMotion(RectTransform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.anchoredPosition;
        protected override void SetValue(Vector3 value) => item.anchoredPosition = value;
    }
}