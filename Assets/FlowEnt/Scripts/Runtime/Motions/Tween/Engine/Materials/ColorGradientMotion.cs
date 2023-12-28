using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the <see cref="Material.color" /> value using a gradient.
    /// </summary>
    public class ColorGradientMotion : AbstractColorGradientMotion<MaterialBuilder>
    {
        [Serializable]
        public class Builder : AbstractGradientBuilder
        {
            public override ITweenMotion Build()
                => new ColorGradientMotion(item, gradient);
        }

        private ColorGradientMotion(MaterialBuilder item, Gradient gradient) : base(item, gradient)
        {
        }

        public ColorGradientMotion(Material item, Gradient gradient) : this(new MaterialBuilder(item), gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.BuiltMaterial.color = gradient.Evaluate(t);
        }
    }
}