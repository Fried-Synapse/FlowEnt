using FriedSynapse.FlowEnt.Motions;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class CommonExtensions
    {
        public static Tween Restart(this Tween tween)
            => tween.Stop().Reset().Start();

        public static Flow Restart(this Flow flow)
            => flow.Stop().Reset().Start();

        public static TweenMotion<T> Tween<T>(this T item, float time = FlowEnt.Tween.DefaultTime, bool autoStart = FlowEnt.Tween.DefaultAutoStart)
            => new Tween(time, autoStart).For(item);

        public static TweenMotion<T> Tween<T>(this T item, TweenOptions options)
            => new Tween(options).For(item);

        public static TweenMotion<TTarget> ForComponent<TSource, TTarget>(this TweenMotion<TSource> wrapper)
            where TSource : Component
            where TTarget : Component
            => wrapper.For(wrapper.Item.GetComponent<TTarget>());

        public static Tween Debug(this Tween tween, string name)
            => tween.Apply(new DebugMotion(name));
    }
}
