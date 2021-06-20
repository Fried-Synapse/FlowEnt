using UnityEngine;

namespace FlowEnt
{
    public static class FlowExtensions
    {
        public static MotionWrapper<T> Tween<T>(this T item, float time, bool autoStart = true)
            => new Tween(time, autoStart).For(item);

        public static MotionWrapper<TTarget> ForComponent<TSource, TTarget>(this MotionWrapper<TSource> wrapper)
            where TSource : Component
            where TTarget : Component
            => wrapper.For(wrapper.Item.GetComponent<TTarget>());
    }
}
