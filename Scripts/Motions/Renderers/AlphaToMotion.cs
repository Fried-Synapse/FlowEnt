using UnityEngine;

namespace FlowEnt.Motions.Renderers
{
    public class AlphaToMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public AlphaToMotion(TRenderer item, float to) : base(item)
        {
            To = to;
        }

        public AlphaToMotion(TRenderer item, float from, float to) : this(item, to)
        {
            From = from;
        }

        public float? From { get; private set; }
        public float To { get; }
        private Color color;

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.material.color.a;
            }
            else
            {
                Color color = Item.material.color;
                color.a = From.Value;
                Item.material.color = color;
            }
        }

        public override void OnUpdate(float t)
        {
            color = Item.material.color;
            color.a = Mathf.Lerp(From.Value, To, t);
            Item.material.color = color;
        }
    }
}