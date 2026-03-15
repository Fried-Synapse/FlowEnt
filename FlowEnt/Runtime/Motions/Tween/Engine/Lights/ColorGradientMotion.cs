using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Lights
{
    /// <summary>
    /// Lerps the <see cref="Light.color" /> value using a gradient.
    /// </summary>
    public class ColorGradientMotion : AbstractColorGradientMotion<Light>
    {
        [Serializable]
        public class Builder : AbstractGradientBuilder
        {
            public override AbstractTweenMotion Build()
                => new ColorGradientMotion(item, gradient);
        }

        public ColorGradientMotion(Light item, Gradient gradient) : base(item, gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.color = gradient.Evaluate(t);
        }
    }
}