using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the <see cref="Material.color" /> value using a gradient.
    /// </summary>
    public class ColorGradientMotion : AbstractColorGradientMotion<Material>
    {
        [Serializable]
        public class Builder : AbstractGradientBuilder
        {
            public override ITweenMotion Build()
                => new ColorGradientMotion(item, gradient);
        }

        public ColorGradientMotion(Material item, Gradient gradient) : base(item, gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.color = gradient.Evaluate(t);
        }
    }
}