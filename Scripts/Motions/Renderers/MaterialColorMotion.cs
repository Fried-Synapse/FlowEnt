using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class MaterialColorMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialColorMotion(TRenderer item, string propertyName, Color value) : base(item)
        {
            this.propertyName = propertyName;
            this.value = value;
        }

        private readonly string propertyName;
        private readonly Color value;
        private Color from;
        private Color to;

        public override void OnStart()
        {
            from = item.material.GetColor(propertyName);
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.material.SetColor(propertyName, Color.LerpUnclamped(from, to, t));
        }
    }
}