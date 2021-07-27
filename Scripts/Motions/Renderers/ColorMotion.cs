using UnityEngine;

namespace FlowEnt.Motions.Renderers
{
    public class ColorMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public ColorMotion(TRenderer item, Color value) : base(item)
        {
            Value = value;
        }

        public Color Value { get; }
        public Color? From { get; private set; }
        public Color? To { get; private set; }

        public override void OnStart()
        {
            From = Item.material.color;
            To = From + Value;
        }

        public override void OnUpdate(float t)
        {
            Item.material.color = Color.Lerp(From.Value, To.Value, t);
        }
    }
}