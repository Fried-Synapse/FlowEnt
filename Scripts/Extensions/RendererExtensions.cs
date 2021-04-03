using UnityEngine;

namespace FlowEnt
{
    public static class RendererExtensions
    {
        public static MotionWrapper<TRenderer> Alpha<TRenderer>(this TRenderer renderer, float to, float time)
            where TRenderer : Renderer
            => ExtensionsHelper.PrepareMotion(renderer, time).Alpha(to);

        public static MotionWrapper<TRenderer> Alpha<TRenderer>(this TRenderer renderer, float from, float to, float time)
            where TRenderer : Renderer
            => ExtensionsHelper.PrepareMotion(renderer, time).Alpha(from, to);

        public static MotionWrapper<TRenderer> Color<TRenderer>(this TRenderer renderer, Color to, float time)
            where TRenderer : Renderer
            => ExtensionsHelper.PrepareMotion(renderer, time).Color(to);

        public static MotionWrapper<TRenderer> Color<TRenderer>(this TRenderer renderer, Color from, Color to, float time)
            where TRenderer : Renderer
            => ExtensionsHelper.PrepareMotion(renderer, time).Color(from, to);
    }
}
