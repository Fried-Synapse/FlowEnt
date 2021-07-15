using UnityEngine;

namespace FlowEnt
{
    public class ScaleSizeDeltaToMotion : AbstractMotion<RectTransform>
    {
        public ScaleSizeDeltaToMotion(RectTransform item, Vector2 to) : base(item)
        {
            To = to;
        }

        public ScaleSizeDeltaToMotion(RectTransform item, Vector2 from, Vector2 to) : this(item, to)
        {
            From = from;
        }

        public Vector2? From { get; private set; }
        public Vector2 To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.sizeDelta;
            }
            else
            {
                Item.sizeDelta = From.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.sizeDelta = Vector3.Lerp(From.Value, To, t);
        }
    }
}