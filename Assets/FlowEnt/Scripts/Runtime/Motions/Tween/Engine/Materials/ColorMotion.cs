using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Materials
{
    /// <summary>
    /// Lerps the <see cref="Material.color" /> value.
    /// </summary>
    /// <typeparam name="TMaterial"></typeparam>
    public class ColorMotion<TMaterial> : AbstractColorMotion<TMaterial>
        where TMaterial : Material
    {
        public ColorMotion(TMaterial item, Color value) : base(item, value)
        {
        }

        public ColorMotion(TMaterial item, Color? from, Color to) : base(item, from, to)
        {
        }

        protected override Color GetFrom() => item.color;
        protected override void SetValue(Color value) => item.color = value;
    }
}