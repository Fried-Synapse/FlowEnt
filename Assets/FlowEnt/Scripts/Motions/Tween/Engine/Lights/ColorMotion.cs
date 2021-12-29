using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Lights
{
    /// <summary>
    /// Lerps the <see cref="Light.color" /> value.
    /// </summary>
    public class ColorMotion : AbstractColorMotion<Light>
    {
        public ColorMotion(Light item, Color value) : base(item, value)
        {
        }

        public ColorMotion(Light item, Color? from, Color to) : base(item, from, to)
        {
        }

        protected override Color GetFrom() => item.color;
        protected override void SetValue(Color value) => item.color = value;
    }
}