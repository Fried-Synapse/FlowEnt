using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms
{
    public class MoveAnchorMotion : AbstractFloatMinMaxMotion<RectTransform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveAnchorMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveAnchorMotion(item, From, to);
        }

        public MoveAnchorMotion(RectTransform item, MinMaxVector2 value) : base(item, value)
        {
        }

        public MoveAnchorMotion(RectTransform item, MinMaxVector2? from, MinMaxVector2 to) : base(item, from, to)
        {
        }

        public MoveAnchorMotion(RectTransform item, AnchorPresetData value)
            : this(item, new MinMaxVector2(value.Min, value.Max))
        {
        }

        public MoveAnchorMotion(RectTransform item, AnchorPresetData from, AnchorPresetData to)
            : this(item,
                from == null ? null : new MinMaxVector2(from.Min, from.Max),
                new MinMaxVector2(to.Min, to.Max))
        {
        }

        public MoveAnchorMotion(RectTransform item, AnchorPreset value)
            : this(item, AnchorPresetFactory.GetAnchors(value))
        {
        }

        public MoveAnchorMotion(RectTransform item, AnchorPreset? from, AnchorPreset to)
            : this(item,
                from != null ? AnchorPresetFactory.GetAnchors(from.Value) : null,
                AnchorPresetFactory.GetAnchors(to))
        {
        }

        protected override MinMaxVector2 GetFrom() => new(item.anchorMin, item.anchorMax);

        protected override void SetValue(MinMaxVector2 value)
        {
            item.anchorMin = value.Min;
            item.anchorMax = value.Max;
        }
    }
}