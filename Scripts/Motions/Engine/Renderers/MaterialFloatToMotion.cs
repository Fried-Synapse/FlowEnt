using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class MaterialFloatToMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialFloatToMotion(TRenderer item, string propertyName, float to) : base(item)
        {
            this.propertyName = propertyName;
            this.to = to;
        }

        public MaterialFloatToMotion(TRenderer item, string propertyName, float from, float to) : this(item, propertyName, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly string propertyName;
        private readonly bool hasFrom;
        private float from;
        private readonly float to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.material.GetFloat(propertyName);
            }
        }

        public override void OnUpdate(float t)
        {
            item.material.SetFloat(propertyName, Mathf.LerpUnclamped(from, to, t));
        }
    }
}