using UnityEngine;

namespace FlowEnt.Motions.RendererMotions
{
    public class AlphaMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public AlphaMotion(TRenderer item, float value) : base(item)
        {
            Value = value;
        }

        public float Value { get; }
        public float? From { get; private set; }
        public float? To { get; private set; }
        private Color color;

        public override void OnStart()
        {
            From = Item.material.color.a;
            To = From + Value;
        }

        public override void OnUpdate(float t)
        {
            color = Item.material.color;
            color.a = Mathf.Lerp(From.Value, To.Value, t);
            Item.material.color = color;
        }

        public override void OnComplete()
        {
        }
    }
}