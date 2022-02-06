using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the <see cref="Material.color" /> value using a gradient.
    /// </summary>
    /// <typeparam name="TMaterial"></typeparam>
    public class ColorGradientMotion<TMaterial> : AbstractGradientMotion<TMaterial>
        where TMaterial : Material
    {
        public ColorGradientMotion(TMaterial item, Gradient gradient) : base(item, gradient)
        {
        }

        public override void OnUpdate(float t)
        {
            item.color = gradient.Evaluate(t);
        }
    }
}