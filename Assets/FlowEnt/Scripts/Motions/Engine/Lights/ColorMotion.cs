using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Lights
{
    public class ColorMotion : AbstractMotion<Light>
    {
        public ColorMotion(Light item, Color value) : base(item)
        {
            this.value = value;
        }

        private readonly Color value;
        private Color from;
        private Color to;

        public override void OnStart()
        {
            from = item.color;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.color = Color.LerpUnclamped(from, to, t);
        }
    }
}