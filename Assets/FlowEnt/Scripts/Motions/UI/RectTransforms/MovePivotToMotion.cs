using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.UI.RectTransforms
{
    public class MovePivotToMotion : AbstractMotion<RectTransform>
    {
        public MovePivotToMotion(RectTransform item, Vector2 to) : base(item)
        {
            this.to = to;
        }

        public MovePivotToMotion(RectTransform item, Vector2 from, Vector2 to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        public MovePivotToMotion(RectTransform item, PivotPreset to) : base(item)
        {
            this.to = PivotPresetFactory.GetPivot(to);
        }

        public MovePivotToMotion(RectTransform item, PivotPreset from, PivotPreset to) : this(item, to)
        {
            hasFrom = true;
            this.from = PivotPresetFactory.GetPivot(from);
        }

        private readonly bool hasFrom;
        private Vector2 from;
        private readonly Vector2 to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.pivot;
            }
        }

        public override void OnUpdate(float t)
        {
            item.pivot = Vector2.LerpUnclamped(from, to, t);
        }
    }
}