using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.RectTransforms
{
    public class MoveAnchorToMotion : AbstractTweenMotion<RectTransform>
    {
        public MoveAnchorToMotion(RectTransform item, Vector2 toMin, Vector2 toMax) : base(item)
        {
            this.toMin = toMin;
            this.toMax = toMax;
        }

        public MoveAnchorToMotion(RectTransform item, Vector2 fromMin, Vector2 fromMax, Vector2 toMin, Vector2 toMax) : this(item, toMin, toMax)
        {
            hasFrom = true;
            this.fromMin = fromMin;
            this.fromMax = fromMax;
        }

        public MoveAnchorToMotion(RectTransform item, AnchorPreset to) : base(item)
        {
            AnchorPresetData toData = AnchorPresetFactory.GetAnchors(to);
            toMin = toData.Min;
            toMax = toData.Max;
        }

        public MoveAnchorToMotion(RectTransform item, AnchorPreset from, AnchorPreset to) : this(item, to)
        {
            hasFrom = true;
            AnchorPresetData fromData = AnchorPresetFactory.GetAnchors(from);
            fromMin = fromData.Min;
            fromMax = fromData.Max;
        }

        private readonly bool hasFrom;
        private Vector2 fromMin;
        private Vector2 fromMax;
        private readonly Vector2 toMin;
        private readonly Vector2 toMax;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                fromMin = item.anchorMin;
                fromMax = item.anchorMax;
            }
        }

        public override void OnUpdate(float t)
        {
            item.anchorMin = Vector2.LerpUnclamped(fromMin, toMin, t);
            item.anchorMax = Vector2.LerpUnclamped(fromMax, toMax, t);
        }
    }
}