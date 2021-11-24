using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class MaterialFloatMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialFloatMotion(TRenderer item, string propertyName, float value) : base(item)
        {
            this.propertyName = propertyName;
            this.value = value;
        }

        private readonly string propertyName;
        private readonly float value;
        private float from;
        private float to;

        public override void OnStart()
        {
            from = item.material.GetFloat(propertyName);
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.material.SetFloat(propertyName, Mathf.LerpUnclamped(from, to, t));
        }
    }
}