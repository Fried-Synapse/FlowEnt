using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class MaterialColorToMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialColorToMotion(TRenderer item, string propertyName, Color to) : base(item)
        {
            this.propertyName = propertyName;
            this.to = to;
        }

        public MaterialColorToMotion(TRenderer item, string propertyName, Color from, Color to) : this(item, propertyName, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly string propertyName;
        private readonly bool hasFrom;
        private Color from;
        private readonly Color to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.material.GetColor(propertyName);
            }
        }

        public override void OnUpdate(float t)
        {
            item.material.SetColor(propertyName, Color.LerpUnclamped(from, to, t));
        }
    }
}