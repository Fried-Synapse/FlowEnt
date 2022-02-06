using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Lights
{
    /// <summary>
    /// Lerps the <see cref="Light.shadowStrength" /> value.
    /// </summary>
    public class ShadowStrengthMotion : AbstractFloatMotion<Light>
    {
        public ShadowStrengthMotion(Light item, float value) : base(item, value)
        {
        }

        public ShadowStrengthMotion(Light item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.shadowStrength;
        protected override void SetValue(float value) => item.shadowStrength = value;
    }
}