using TweenDebugMotion = FriedSynapse.FlowEnt.Motions.Tween.DebugMotion;
using EchoDebugMotion = FriedSynapse.FlowEnt.Motions.Echo.DebugMotion;

namespace FriedSynapse.FlowEnt
{
    public static class CommonExtensions
    {
        #region Tween

        /// <summary>
        /// Creates a <see cref="FlowEnt.Tween" /> and a <see cref="TweenMotionProxy{T}" /> for the item with the selected params.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="time"></param>
        /// <param name="autoStart"></param>
        /// <typeparam name="TItem"></typeparam>
        public static TweenMotionProxy<TItem> Tween<TItem>(this TItem item, float time = TweenOptions.DefaultTime, bool autoStart = AbstractAnimationOptions.DefaultAutoStart)
            where TItem : class
            => new Tween(time, autoStart).For(item);

        /// <summary>
        /// Creates a <see cref="FlowEnt.Tween" /> and a <see cref="TweenMotionProxy{T}" /> for the item with the selected options.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="options"></param>
        /// <typeparam name="TItem"></typeparam>
        public static TweenMotionProxy<TItem> Tween<TItem>(this TItem item, TweenOptions options)
            where TItem : class
            => new Tween(options).For(item);

        /// <summary>
        /// Restarts the tween by calling <see cref="Tween.Stop" />, <see cref="Tween.Reset" />, <see cref="Tween.Start" /> in this order.
        /// </summary>
        /// <param name="tween"></param>
        public static Tween Restart(this Tween tween)
            => tween.Stop().Reset().Start();

        /// <summary>
        /// Applies a <see cref="DebugMotion" /> to the tween.
        /// </summary>
        /// <param name="tween"></param>
        /// <param name="name">If name is null, it will use the tween's name. Make you it's set.</param>
        public static Tween Debug(this Tween tween, string name = null)
            => tween.Apply(new TweenDebugMotion(name ?? tween.Name));

        #endregion

        #region Echo

        /// <summary>
        /// Creates a <see cref="FlowEnt.Echo" /> and a <see cref="EchoMotionProxy{T}" /> for the item with the selected params.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="timeout"></param>
        /// <param name="autoStart"></param>
        /// <typeparam name="TItem"></typeparam>
        public static EchoMotionProxy<TItem> Echo<TItem>(this TItem item, float? timeout = default, bool autoStart = AbstractAnimationOptions.DefaultAutoStart)
            where TItem : class
            => new Echo(timeout, autoStart).For(item);

        /// <summary>
        /// Creates a <see cref="FlowEnt.Echo" /> and a <see cref="EchoMotionProxy{T}" /> for the item with the selected options.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="options"></param>
        /// <typeparam name="TItem"></typeparam>
        public static EchoMotionProxy<TItem> Echo<TItem>(this TItem item, EchoOptions options)
            where TItem : class
            => new Echo(options).For(item);

        /// <summary>
        /// Restarts the echo by calling <see cref="Echo.Stop" />, <see cref="Echo.Reset" />, <see cref="Echo.Start" /> in this order.
        /// </summary>
        /// <param name="echo"></param>
        public static Echo Restart(this Echo echo)
            => echo.Stop().Reset().Start();

        /// <summary>
        /// Applies a <see cref="DebugMotion" /> to the echo.
        /// </summary>
        /// <param name="echo"></param>
        /// <param name="name">If name is null, it will use the echo's name. Make you it's set.</param>
        public static Echo Debug(this Echo echo, string name = null)
            => echo.Apply(new EchoDebugMotion(name ?? echo.Name));

        #endregion

        #region Flow

        /// <summary>
        /// Restards the flow by calling <see cref="Flow.Stop" />, <see cref="Flow.Reset" />, <see cref="Flow.Start" /> in this order.
        /// </summary>
        /// <param name="flow"></param>
        public static Flow Restart(this Flow flow)
            => flow.Stop().Reset().Start();

        #endregion
    }
}
