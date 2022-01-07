using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the alpha for <see cref="Material.color" /> value.
    /// </summary>
    /// <typeparam name="TMaterial"></typeparam>
    public class AlphaMotion<TMaterial> : AbstractAlphaMotion<TMaterial>
        where TMaterial : Material
    {
        public AlphaMotion(TMaterial item, float value) : base(item, value)
        {
        }

        public AlphaMotion(TMaterial item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.color.a;
        protected override void SetValue(float value) => item.color = SetAlpha(item.color, value);
    }
}