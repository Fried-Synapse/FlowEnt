using UnityEngine;

namespace FlowEnt.Motions.Renderers
{
    public class ColorToMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public ColorToMotion(TRenderer item, Color to) : base(item)
        {
            To = to;
        }

        public ColorToMotion(TRenderer item, Color from, Color to) : this(item, to)
        {
            From = from;
        }

        public Color? From { get; private set; }
        public Color To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.material.color;
            }
            else
            {
                Item.material.color = From.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.material.color = Color.Lerp(From.Value, To, t);
        }
    }
}