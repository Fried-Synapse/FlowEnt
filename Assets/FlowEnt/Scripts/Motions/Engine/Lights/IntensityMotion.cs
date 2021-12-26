using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Lights
{
    public class IntensityMotion : AbstractFloatMotion<Light>
    {
        public IntensityMotion(Light item, float value) : base(item, value)
        {
        }

        public IntensityMotion(Light item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.intensity;
        protected override void SetValue(float value) => item.intensity = value;
    }
}