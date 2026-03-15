using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms
{
    public class MovePivotMotion : AbstractVector2Motion<RectTransform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new MovePivotMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new MovePivotMotion(item, From, to);
        }

        [Serializable]
        public class PresetValueBuilder : AbstractValueBuilder<PivotPreset>
        {
            public override AbstractTweenMotion Build()
                => new MovePivotMotion(item, value);
        }

        [Serializable]
        public class PresetFromToBuilder : AbstractFromToBuilder<PivotPreset>
        {
            public override AbstractTweenMotion Build()
                => new MovePivotMotion(item, From, to);
        }

        public MovePivotMotion(RectTransform item, Vector2 value) : base(item, value)
        {
        }

        public MovePivotMotion(RectTransform item, Vector2? from, Vector2 to) : base(item, from, to)
        {
        }

        public MovePivotMotion(RectTransform item, PivotPreset value) : base(item,
            PivotPresetFactory.GetPivot(value))
        {
        }

        public MovePivotMotion(RectTransform item, PivotPreset? from, PivotPreset to) : base(item,
            from != null ? PivotPresetFactory.GetPivot(from.Value) : null, PivotPresetFactory.GetPivot(to))
        {
        }

        protected override Vector2 GetFrom() => item.pivot;
        protected override void SetValue(Vector2 value) => item.pivot = value;
    }
}