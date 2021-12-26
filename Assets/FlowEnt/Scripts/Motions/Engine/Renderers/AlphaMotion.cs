using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Renderers
{
    public class AlphaMotion<TRenderer> : AbstractMotion<TRenderer>
        where TRenderer : Renderer
    {
        public AlphaMotion(TRenderer item, float value) : base(item)
        {
            this.value = value;
        }

        private readonly float value;
        private float from;
        private float to;
        private Color colorCache;

        public override void OnStart()
        {
            from = item.material.color.a;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            colorCache = item.material.color;
            colorCache.a = Mathf.LerpUnclamped(from, to, t);
            item.material.color = colorCache;
        }
    }
}