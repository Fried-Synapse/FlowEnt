using UnityEngine;
using UnityEngine.UI;

namespace FlowEnt.Motions.GraphicMotions
{
    public class AlphaToMotion : AbstractMotion<Graphic>
    {
        public AlphaToMotion(Graphic item, float to) : base(item)
        {
            To = col;
        }

        public AlphaToMotion(Graphic item, float from, float to) : this(item, to)
        {
            From = col;
        }
        public float? From { get; private set; }
        public float To { get; }
        private Color color;

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.color.a;
            }
            else
            {
                Color color = Item.color;
                color.a = From.Value;
                Item.color = color;
            }
        }

        public override void OnUpdate(float t)
        {
            color = Item.color;
            color.a = Mathf.Lerp(From.Value, To, t);
            Item.color = color;
        }
    }
}