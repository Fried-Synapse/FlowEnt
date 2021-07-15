using UnityEngine;

namespace FlowEnt.Motions.CanvasGroupMotions
{
    public class AlphaToMotion : AbstractMotion<CanvasGroup>
    {
        public AlphaToMotion(CanvasGroup item, float to) : base(item)
        {
            To = to;
        }

        public AlphaToMotion(CanvasGroup item, float from, float to) : this(item, to)
        {
            From = from;
        }

        public float? From { get; private set; }
        public float To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.alpha;
            }
            else
            {
                Item.alpha = From.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.alpha = Mathf.Lerp(From.Value, To, t);
        }
    }
}