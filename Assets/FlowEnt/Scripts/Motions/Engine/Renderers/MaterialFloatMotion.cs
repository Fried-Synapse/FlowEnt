using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class MaterialFloatMotion<TRenderer> : AbstractFloatMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialFloatMotion(TRenderer item, string propertyName, float value) : base(item, value)
        {
            this.propertyName = propertyName;
        }

        public MaterialFloatMotion(TRenderer item, string propertyName, float? from, float to) : base(item, from, to)
        {
            this.propertyName = propertyName;
        }

        private readonly string propertyName;
        protected override float GetFrom() => item.material.GetFloat(propertyName);
        protected override void SetValue(float value) => item.material.SetFloat(propertyName, value);
    }
}