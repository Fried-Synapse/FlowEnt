using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms
{
    /// <summary>
    /// Lerps the <see cref="RectTransform.anchoredPosition" /> value.
    /// </summary>
    public class MoveAnchoredPositionVectorMotion : AbstractVector3Motion<RectTransform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new MoveAnchoredPositionVectorMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new MoveAnchoredPositionVectorMotion(item, From, to);
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