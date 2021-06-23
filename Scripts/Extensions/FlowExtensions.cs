using System;
using UnityEngine;

namespace FlowEnt
{
    public static class FlowExtensions
    {
        public static MotionWrapper<T> Tween<T>(this T item, TweenOptions options)
            => new Tween(options).For(item);

        public static MotionWrapper<T> Tween<T>(this T item, Func<TweenOptions, TweenOptions> optionsBuilder)
        {
            TweenOptions options = new TweenOptions();
            return new Tween(optionsBuilder == null ? options : optionsBuilder.Invoke(options)).For(item);
        }

        public static MotionWrapper<T> Tween<T>(this T item, float time, Func<TweenOptions, TweenOptions> optionsBuilder = null)
        {
            TweenOptions options = new TweenOptions() { Time = time };
            return new Tween(optionsBuilder == null ? options : optionsBuilder.Invoke(options)).For(item);
        }


        public static MotionWrapper<TTarget> ForComponent<TSource, TTarget>(this MotionWrapper<TSource> wrapper)
            where TSource : Component
            where TTarget : Component
            => wrapper.For(wrapper.Item.GetComponent<TTarget>());
    }
}
