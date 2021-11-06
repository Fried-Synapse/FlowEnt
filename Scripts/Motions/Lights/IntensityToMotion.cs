using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Lights
{
    public class IntensityToMotion : AbstractMotion<Light>
    {
        public IntensityToMotion(Light item, float to) : base(item)
        {
            this.to = to;
        }

        public IntensityToMotion(Light item, float from, float to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly bool hasFrom;
        private float from;
        private readonly float to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.intensity;
            }
        }

        public override void OnUpdate(float t)
        {
            item.intensity = Mathf.LerpUnclamped(from, to, t);
        }
    }
}