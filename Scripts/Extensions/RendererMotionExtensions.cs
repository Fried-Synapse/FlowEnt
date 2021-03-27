using UnityEngine;

namespace FlowEnt
{
    public static class RendererMotionExtensions
    {
        public static Motion<TRenderer> Alpha<TRenderer>(this Motion<TRenderer> motion, float to)
            where TRenderer : Renderer
        {
            float? from = null;
            Color color;
            motion
                .OnStart(() =>
                {
                    from = motion.Element.material.color.a;
                })
                .OnUpdate(t =>
                {
                    color = motion.Element.material.color;
                    color.a = Mathf.Lerp(from.Value, to, t);
                    motion.Element.material.color = color;
                });

            return motion;
        }

        public static Motion<TRenderer> Alpha<TRenderer>(this Motion<TRenderer> motion, float from, float to)
            where TRenderer : Renderer
        {
            Color color;
            motion
                .OnStart(() =>
                {
                    color = motion.Element.material.color;
                    color.a = from;
                    motion.Element.material.color = color;
                })
                .OnUpdate(t =>
                {
                    color = motion.Element.material.color;
                    color.a = Mathf.Lerp(from, to, t);
                    motion.Element.material.color = color;
                });

            return motion;
        }
    }
}