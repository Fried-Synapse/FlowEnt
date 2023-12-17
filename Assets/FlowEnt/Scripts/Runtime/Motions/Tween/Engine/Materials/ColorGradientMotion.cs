using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the <see cref="Material.color" /> value using a gradient.
    /// </summary>
    public class ColorGradientMotion : AbstractColorGradientMotion<DynamicMaterial>
    {
        [Serializable]
        public class Builder : AbstractGradientBuilder
        {
            public override ITweenMotion Build()
                => new ColorGradientMotion(item, gradient);
        }

        private ColorGradientMotion(DynamicMaterial item, Gradient gradient) : base(item, gradient)
        {
        }

        public ColorGradientMotion(Material item, Gradient gradient) : this((DynamicMaterial)item, gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.Material.color = gradient.Evaluate(t);
        }
    }
}