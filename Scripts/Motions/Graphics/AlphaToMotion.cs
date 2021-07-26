using UnityEngine;
using UnityEngine.UI;

namespace FlowEnt.Motions.GraphicMotions
{
    public class AlphaToMotion : AbstractMotion<Graphic>
    {
        public AlphaToMotion(Graphic item, float to) : base(item)
        {
            Color col = item.color;
            col.a = to;
            To = col;
        }

        public AlphaToMotion(Graphic item, float from, float to) : this(item, to)
        {
            Color col = item.color;
            col.a = from;
            From = col;
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