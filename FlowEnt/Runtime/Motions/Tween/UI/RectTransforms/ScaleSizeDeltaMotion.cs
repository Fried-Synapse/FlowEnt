using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms
{
    /// <summary>
    /// Lerps the <see cref="RectTransform.sizeDelta" /> value.
    /// </summary>
    public class ScaleSizeDeltaMotion : AbstractVector2Motion<RectTransform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new ScaleSizeDeltaMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new ScaleSizeDeltaMotion(item, From, to);
        }

        public ScaleSizeDeltaMotion(RectTransform item, Vector2 value) : base(item, value)
        {
        }

        public ScaleSizeDeltaMotion(RectTransform item, Vector2? from, Vector2 to) : base(item, from, to)
        {
        }

        protected override Vector2 GetFrom() => item.sizeDelta;
        protected override Vector2 GetTo(Vector2 from, Vector2 value) => Vector2.Scale(from, value);
        protected override void SetValue(Vector2 value) => item.sizeDelta = value;
    }
}