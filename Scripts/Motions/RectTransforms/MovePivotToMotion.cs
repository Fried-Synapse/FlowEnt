using UnityEngine;

namespace FlowEnt.Motions.RectTransforms
{
    public class MovePivotToMotion : AbstractMotion<RectTransform>
    {
        public MovePivotToMotion(RectTransform item, Vector2 to) : base(item)
        {
            To = to;
        }

        public MovePivotToMotion(RectTransform item, Vector2 from, Vector2 to) : this(item, to)
        {
            From = from;
        }

        public MovePivotToMotion(RectTransform item, PivotPreset to) : base(item)
        {
            To = PivotPresetFactory.GetPivot(to);
        }

        public MovePivotToMotion(RectTransform item, PivotPreset from, PivotPreset to) : this(item, to)
        {
            From = PivotPresetFactory.GetPivot(from);
        }

        public Vector2? From { get; private set; }
        public Vector2 To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.pivot;
            }
            else
            {
                Item.pivot = From.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.pivot = Vector2.LerpUnclamped(From.Value, To, t);
        }
    }
}