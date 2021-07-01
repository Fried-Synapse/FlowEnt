using System;
using UnityEngine;

namespace FlowEnt
{
    public static class CommonExtensions
    {
        public static TweenMotion<T> Tween<T>(this T item)
            => new Tween(true).For(item);

        public static TweenMotion<T> Tween<T>(this T item, float time)
            => new Tween(time, true).For(item);

        public static TweenMotion<T> Tween<T>(this T item, TweenOptions options)
            => new Tween(options).For(item);

        public static TweenMotion<TTarget> ForComponent<TSource, TTarget>(this TweenMotion<TSource> wrapper)
            where TSource : Component
            where TTarget : Component
            => wrapper.For(wrapper.Item.GetComponent<TTarget>());
    }
}
