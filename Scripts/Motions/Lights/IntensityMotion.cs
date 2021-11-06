using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Lights
{
    public class IntensityMotion : AbstractMotion<Light>
    {
        public IntensityMotion(Light item, float value) : base(item)
        {
            this.value = value;
        }

        private readonly float value;
        private float from;
        private float to;

        public override void OnStart()
        {
            from = item.intensity;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.intensity = Mathf.LerpUnclamped(from, to, t);
        }
    }
}