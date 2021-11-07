using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class MaterialAlphaToMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialAlphaToMotion(TRenderer item, string propertyName, float to) : base(item)
        {
            this.propertyName = propertyName;
            this.to = to;
        }

        public MaterialAlphaToMotion(TRenderer item, string propertyName, float from, float to) : this(item, propertyName, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly string propertyName;
        private readonly bool hasFrom;
        private float from;
        private readonly float to;
        private Color colorCache;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.material.GetColor(propertyName).a;
            }
        }

        public override void OnUpdate(float t)
        {
            colorCache = item.material.GetColor(propertyName);
            colorCache.a = Mathf.LerpUnclamped(from, to, t);
            item.material.SetColor(propertyName, colorCache);
        }
    }
}