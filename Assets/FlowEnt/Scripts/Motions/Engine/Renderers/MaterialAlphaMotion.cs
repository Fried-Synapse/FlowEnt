using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class MaterialAlphaMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public MaterialAlphaMotion(TRenderer item, string propertyName, float value) : base(item)
        {
            this.propertyName = propertyName;
            this.value = value;
        }

        private readonly string propertyName;
        private readonly float value;
        private float from;
        private float to;
        private Color colorCache;

        public override void OnStart()
        {
            from = item.material.GetColor(propertyName).a;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            colorCache = item.material.GetColor(propertyName);
            colorCache.a = Mathf.LerpUnclamped(from, to, t);
            item.material.SetColor(propertyName, colorCache);
        }
    }
}