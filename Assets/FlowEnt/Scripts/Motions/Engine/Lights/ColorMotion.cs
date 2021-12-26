using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Lights
{
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