using System;
using UnityEngine;

namespace FlowEnt
{
    public static class FlowExtensions
    {
        public static TweenMotion<T> Tween<T>(this T item, TweenOptions options)
            => new Tween(options).For(item);

        public static TweenMotion<T> Tween<T>(this T item, Func<TweenOptions, TweenOptions> optionsBuilder)
        {
            TweenOptions options = new TweenOptions() { AutoStart = true };
            return new Tween(optionsBuilder == null ? options : optionsBuilder.Invoke(options)).For(item);
        }

        public static TweenMotion<T> Tween<T>(this T item, float time, Func<TweenOptions, TweenOptions> optionsBuilder = null)
        {
            TweenOptions options = new TweenOptions() { Time = time, AutoStart = true };
            return new Tween(optionsBuilder == null ? options : optionsBuilder.Invoke(options)).For(item);
        }


        public static TweenMotion<TTarget> ForComponent<TSource, TTarget>(this TweenMotion<TSource> wrapper)
            where TSource : Component
            where TTarget : Component
            => wrapper.For(wrapper.Item.GetComponent<TTarget>());
    }
}
