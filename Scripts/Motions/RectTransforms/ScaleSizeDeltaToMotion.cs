using UnityEngine;

namespace FlowEnt
{
    public class ScaleSizeDeltaToMotion : AbstractMotion<RectTransform>
    {
        public ScaleSizeDeltaToMotion(RectTransform item, Vector2 to) : base(item)
        {
            this.to = to;
        }

        public ScaleSizeDeltaToMotion(RectTransform item, Vector2 from, Vector2 to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly bool hasFrom;
        private Vector2 from;
        private readonly Vector2 to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.sizeDelta;
            }
        }

        public override void OnUpdate(float t)
        {
            item.sizeDelta = Vector3.Lerp(from, to, t);
        }
    }
}