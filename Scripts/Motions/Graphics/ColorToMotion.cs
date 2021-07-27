using UnityEngine;
using UnityEngine.UI;

namespace FlowEnt.Motions.GraphicMotions
{
    public class ColorToMotions : AbstractMotion<TGraphic> where TGraphic : Graphic
    {
        public ColorToMotion(TGraphic item, Color to) : base(item)
        {
            To = to;
        }

        public ColorToMotion(TGraphic item, Color from, Color to) : this(item, to)
        {
            From = from;
        }

        public Color? From { get; private set; }
        public Color To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.color;
            }
            else
            {
                Item.color = From.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.color = Color.Lerp(From.Value, To, t);
        }
    }
}