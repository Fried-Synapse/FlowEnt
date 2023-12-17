using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the color for the specified shader property using a gradient.
    /// </summary>
    public class ColorGradientPropertyMotion : AbstractColorGradientMotion<DynamicMaterialWithProperty<Color>>
    {
        [Serializable]
        public class GradientBuilder : AbstractGradientBuilder
        {
            public override ITweenMotion Build()
                => new ColorGradientPropertyMotion(item, gradient);
        }
        
        private ColorGradientPropertyMotion(DynamicMaterialWithProperty<Color> item, Gradient gradient)
            : base(item, gradient)
        {
        }
        
        public ColorGradientPropertyMotion(Material item, string propertyName, Gradient gradient)
            : this(item, Shader.PropertyToID(propertyName), gradient)
        {
        }

        public ColorGradientPropertyMotion(Material item, int propertyId, Gradient gradient)
            : this(new DynamicMaterialWithProperty<Color>(item, propertyId), gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.Material.SetColor(item.PropertyId, gradient.Evaluate(t));
        }
    }
}