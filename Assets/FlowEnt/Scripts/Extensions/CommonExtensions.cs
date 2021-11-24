using FriedSynapse.FlowEnt.Motions;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Create a <see cref="FlowEnt.Tween" /> and a <see cref="TweenMotion{T}" /> for the item with the selected params.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="time"></param>
        /// <param name="autoStart"></param>
        /// <typeparam name="T"></typeparam>
        public static TweenMotion<T> Tween<T>(this T item, float time = FlowEnt.Tween.DefaultTime, bool autoStart = FlowEnt.Tween.DefaultAutoStart)
            => new Tween(time, autoStart).For(item);

        /// <summary>
        /// Create a <see cref="FlowEnt.Tween" /> and a <see cref="TweenMotion{T}" /> for the item with the selected options.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="options"></param>
        /// <typeparam name="T"></typeparam>
        public static TweenMotion<T> Tween<T>(this T item, TweenOptions options)
            => new Tween(options).For(item);

        /// <summary>
        /// Restards the tween by calling <see cref="Tween.Stop" />, <see cref="Tween.Reset" />, <see cref="Tween.Start" /> in this order.
        /// </summary>
        /// <param name="tween"></param>
        public static Tween Restart(this Tween tween)
            => tween.Stop().Reset().Start();

        /// <summary>
        /// Restards the flow by calling <see cref="Flow.Stop" />, <see cref="Flow.Reset" />, <see cref="Flow.Start" /> in this order.
        /// </summary>
        /// <param name="flow"></param>
        public static Flow Restart(this Flow flow)
            => flow.Stop().Reset().Start();

        /// <summary>
        /// Applies a <see cref="DebugMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="name">If name is null, it will use the tween's name. Make you it's set.</param>
        public static Tween Debug(this Tween tween, string name = null)
            => tween.Apply(new DebugMotion(name ?? tween.Name));
    }
}
