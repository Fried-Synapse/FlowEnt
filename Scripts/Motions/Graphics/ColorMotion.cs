using UnityEngine;
using UnityEngine.UI;

namespace FlowEnt.Motions.GraphicMotions
{
    public class ColorMotions : AbstractMotion<Graphic>
    {
        public ColorMotion(Graphic item, Color value) : base(item)
        {
            Value = value;
        }

        public Color Value { get; }
        public Color? From { get; private set; }
        public Color? To { get; private set; }

        public override void OnStart()
        {
            From = Item.color;
            To = From + Value;
        }

        public override void OnUpdate(float t)
        {
            Item.color = Mathf.Lerp(From.Value, To.Value, t); ;
        }
    }
}