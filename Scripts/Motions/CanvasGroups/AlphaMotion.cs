using UnityEngine;

namespace FlowEnt.Motions.CanvasGroups
{
    public class AlphaMotion : AbstractMotion<CanvasGroup>
    {
        public AlphaMotion(CanvasGroup item, float value) : base(item)
        {
            Value = value;
        }

        public float Value { get; }
        public float? From { get; private set; }
        public float? To { get; private set; }

        public override void OnStart()
        {
            From = Item.alpha;
            To = From + Value;
        }

        public override void OnUpdate(float t)
        {
            Item.alpha = Mathf.Lerp(From.Value, To.Value, t);
        }
    }
}