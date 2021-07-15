using UnityEngine;

namespace FlowEnt.Motions.RectTransformMotions
{
    public class MoveAnchorToMotion : AbstractMotion<RectTransform>
    {
        public MoveAnchorToMotion(RectTransform item, Vector2 toMin, Vector2 toMax) : base(item)
        {
            ToMin = toMin;
            ToMax = toMax;
        }

        public MoveAnchorToMotion(RectTransform item, Vector2 fromMin, Vector2 fromMax, Vector2 toMin, Vector2 toMax) : this(item, toMin, toMax)
        {
            FromMin = fromMin;
            FromMax = fromMax;
        }

        public MoveAnchorToMotion(RectTransform item, AnchorPreset to) : base(item)
        {
            AnchorPresetData toData = AnchorPresetFactory.GetAnchors(to);
            ToMin = toData.Min;
            ToMax = toData.Max;
        }

        public MoveAnchorToMotion(RectTransform item, AnchorPreset from, AnchorPreset to) : this(item, to)
        {
            AnchorPresetData fromData = AnchorPresetFactory.GetAnchors(from);
            FromMin = fromData.Min;
            FromMax = fromData.Max;
        }

        public Vector2? FromMin { get; private set; }
        public Vector2? FromMax { get; private set; }
        public Vector2 ToMin { get; }
        public Vector2 ToMax { get; }

        public override void OnStart()
        {
            //if one is null, we good
            if (FromMin == null)
            {
                FromMin = Item.anchorMin;
                FromMax = Item.anchorMax;
            }
            else
            {
                Item.anchorMin = FromMin.Value;
                Item.anchorMax = FromMax.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.anchorMin = Vector2.LerpUnclamped(FromMin.Value, ToMin, t);
            Item.anchorMax = Vector2.LerpUnclamped(FromMax.Value, ToMax, t);
        }
    }
}