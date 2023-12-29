using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Lights
{
    /// <summary>
    /// Lerps the <see cref="Light.intensity" /> value.
    /// </summary>
    public class IntensityMotion : AbstractFloatMotion<Light>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new IntensityMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new IntensityMotion(item, From, to);
        }

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