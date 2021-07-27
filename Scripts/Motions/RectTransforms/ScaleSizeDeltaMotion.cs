using UnityEngine;

namespace FlowEnt
{
    public class ScaleSizeDeltaMotion : AbstractMotion<RectTransform>
    {
        public ScaleSizeDeltaMotion(RectTransform item, Vector2 value) : base(item)
        {
            Value = value;
        }

        public Vector2 Value { get; }
        public Vector2? From { get; private set; }
        public Vector2? To { get; private set; }

        public override void OnStart()
        {
            From = Item.sizeDelta;
            To = Vector2.Scale(From.Value, Value);
        }

        public override void OnUpdate(float t)
        {
            Item.sizeDelta = Vector2.Lerp(From.Value, To.Value, t);
        }
    }
}