using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms
{
    public class MoveAnchorMotion : AbstractTweenMotion<RectTransform>
    {
        [Serializable]
        public class ValueBuilder : AbstractBuilder
        {
            [SerializeField]
            protected Vector2 valueMin;

            [SerializeField]
            protected Vector2 valueMax;

            public override ITweenMotion Build()
                => new MoveAnchorMotion(item, valueMin, valueMax);
        }

        [Serializable]
        public class FromToBuilder : AbstractBuilder
        {
            [SerializeField]
            protected Vector2 fromMin;

            [SerializeField]
            protected Vector2 fromMax;

            [SerializeField]
            protected Vector2 toMin;

            [SerializeField]
            protected Vector2 toMax;

            public override ITweenMotion Build()
                => new MoveAnchorMotion(item, fromMin, fromMax, toMin, toMax);
        }

        public MoveAnchorMotion(RectTransform item, Vector2 valueMin, Vector2 valueMax) : base(item)
        {
            this.valueMin = valueMin;
            this.valueMax = valueMax;
        }

        public MoveAnchorMotion(RectTransform item, Vector2? fromMin, Vector2? fromMax, Vector2 toMin, Vector2 toMax) :
            this(item, toMin, toMax)
        {
            hasFrom = fromMin != null && fromMax != null;
            if (hasFrom)
            {
                this.fromMin = fromMin.Value;
                this.fromMax = fromMax.Value;
            }

            hasTo = true;
            this.toMin = toMin;
            this.toMax = toMax;
        }

        public MoveAnchorMotion(RectTransform item, AnchorPresetData value) : this(item, value.Min, value.Max)
        {
        }

        public MoveAnchorMotion(RectTransform item, AnchorPreset value) : this(item,
            AnchorPresetFactory.GetAnchors(value))
        {
        }

        public MoveAnchorMotion(RectTransform item, AnchorPresetData from, AnchorPresetData to) : this(item,
            from?.Min, from?.Max, to.Min, to.Max)
        {
        }

        public MoveAnchorMotion(RectTransform item, AnchorPreset? from, AnchorPreset to) : this(item,
            from != null ? AnchorPresetFactory.GetAnchors(from.Value) : null, AnchorPresetFactory.GetAnchors(to))
        {
        }

        private readonly bool hasFrom;
        private readonly bool hasTo;
        private readonly Vector2 valueMin;
        private readonly Vector2 valueMax;
        private Vector2 fromMin;
        private Vector2 fromMax;
        private Vector2 toMin;
        private Vector2 toMax;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                fromMin = item.anchorMin;
                fromMax = item.anchorMax;
            }

            if (!hasTo)
            {
                toMin = fromMin + valueMin;
                toMax = fromMax + valueMax;
            }
        }

        public override void OnUpdate(float t)
        {
            item.anchorMin = Vector2.LerpUnclamped(fromMin, toMin, t);
            item.anchorMax = Vector2.LerpUnclamped(fromMax, toMax, t);
        }
    }
}