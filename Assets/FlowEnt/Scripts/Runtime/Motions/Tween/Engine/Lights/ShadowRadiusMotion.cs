using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Lights
{
#if !UNITY_WEBGL
    /// <summary>
    /// Lerps the <see cref="Light.shadowRadius" /> value.
    /// </summary>
    public class ShadowRadiusMotion : AbstractFloatMotion<Light>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new ShadowRadiusMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new ShadowRadiusMotion(item, from, to);
        }

        public ShadowRadiusMotion(Light item, float value) : base(item, value)
        {
        }

        public ShadowRadiusMotion(Light item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.shadowRadius;
        protected override void SetValue(float value) => item.shadowRadius = value;
    }
#endif
}