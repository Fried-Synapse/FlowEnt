using UnityEngine;

namespace FlowEnt
{
    public static class RendererMotionExtensions
    {
        public static MotionWrapper<TRenderer> Alpha<TRenderer>(this MotionWrapper<TRenderer> motion, float to)
            where TRenderer : Renderer
        {
            float? from = null;
            Color color;
            motion
                .OnStart(() =>
                {
                    from = motion.Item.material.color.a;
                })
                .OnUpdate(t =>
                {
                    color = motion.Item.material.color;
                    color.a = Mathf.Lerp(from.Value, to, t);
                    motion.Item.material.color = color;
                });

            return motion;
        }

        public static MotionWrapper<TRenderer> Alpha<TRenderer>(this MotionWrapper<TRenderer> motion, float from, float to)
            where TRenderer : Renderer
        {
            Color color;
            motion
                .OnStart(() =>
                {
                    color = motion.Item.material.color;
                    color.a = from;
                    motion.Item.material.color = color;
                })
                .OnUpdate(t =>
                {
                    color = motion.Item.material.color;
                    color.a = Mathf.Lerp(from, to, t);
                    motion.Item.material.color = color;
                });

            return motion;
        }
    }
}