using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.UI.RectTransforms
{
    public class ScaleSizeDeltaMotion : AbstractMotion<RectTransform>
    {
        public ScaleSizeDeltaMotion(RectTransform item, Vector2 value) : base(item)
        {
            this.value = value;
        }

        private readonly Vector2 value;
        private Vector2 from;
        private Vector2 to;

        public override void OnStart()
        {
            from = item.sizeDelta;
            to = Vector2.Scale(from, value);
        }

        public override void OnUpdate(float t)
        {
            item.sizeDelta = Vector2.LerpUnclamped(from, to, t);
        }
    }
}