using UnityEngine;
using UnityEngine.UI;

namespace FlowEnt.Motions.GraphicMotions
{
    public class AlphaMotion : AbstractMotion<Graphic>
    {
        public AlphaMotion(Graphic item, float value) : base(item)
        {
            Value = value;
        }

        public float Value { get; }
        public float? From { get; private set; }
        public float? To { get; private set; }
        private Color color;

        public override void OnStart()
        {
            From = Item.color.a;
            To = From + Value;
        }

        public override void OnUpdate(float t)
        {
            color = Item.color;
            color.a = Mathf.Lerp(From.Value, To.Value, t);
            Item.color = color;
        }
    }
}