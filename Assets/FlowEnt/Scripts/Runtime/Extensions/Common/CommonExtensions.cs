using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweenDebugMotion = FriedSynapse.FlowEnt.Motions.Tween.DebugMotion;
using EchoDebugMotion = FriedSynapse.FlowEnt.Motions.Echo.DebugMotion;

namespace FriedSynapse.FlowEnt
{
    public static class CommonExtensions
    {
        #region Animation

        /// <summary>
        /// Restarts the animation by calling <see cref="AbstractAnimation.Stop" />, <see cref="AbstractAnimation.Reset" />, <see cref="AbstractAnimation.Start" /> in this order.
        /// </summary>
        /// <param name="animation"></param>
        public static TAnimation Restart<TAnimation>(this TAnimation animation)
            where TAnimation : AbstractAnimation
            => (TAnimation)animation.Stop().Reset().Start();

        #endregion

        #region Tween

        /// <summary>
        /// Creates a <see cref="FlowEnt.Tween" /> and a <see cref="TweenMotionProxy{T}" /> for the item with the selected params.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="time"></param>
        /// <param name="autoStart"></param>
        /// <typeparam name="TItem"></typeparam>
        public static TweenMotionProxy<TItem> Tween<TItem>(this TItem item, float time = TweenOptions.DefaultTime,
            bool autoStart = AbstractAnimationOptions.DefaultAutoStart)
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
        /// Applies a <see cref="TweenDebugMotion" /> to the tween.
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
        public static EchoMotionProxy<TItem> Echo<TItem>(this TItem item, float? timeout = default,
            bool autoStart = AbstractAnimationOptions.DefaultAutoStart)
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
        /// Applies a <see cref="EchoDebugMotion" /> to the echo.
        /// </summary>
        /// <param name="echo"></param>
        /// <param name="name">If name is null, it will use the echo's name. Make you it's set.</param>
        public static Echo Debug(this Echo echo, string name = null)
            => echo.Apply(new EchoDebugMotion(name ?? echo.Name));

        #endregion

        #region List

        public static IEnumerable<AbstractAnimation> Build(
            this IEnumerable<IAbstractAnimationBuilder> animationsBuilders)
        {
            //NOTE if we don't convert to list here somehow it duplicates each built animation is it starts the first one, but returns the second one. Odd stuff.
            List<AbstractAnimation> list = new List<AbstractAnimation>();
            foreach (IAbstractAnimationBuilder animationBuilder in animationsBuilders)
            {
                list.Add(animationBuilder.Build());
            }

            return list;
        }

        public static IEnumerable<AbstractAnimation> Start(this IEnumerable<AbstractAnimation> animations)
        {
            foreach (AbstractAnimation animation in animations)
            {
                animation.Start();
            }

            return animations;
        }

        public static async Task<IEnumerable<AbstractAnimation>> StartAsync(
            this IEnumerable<AbstractAnimation> animations)
        {
            List<Task> tasks = new List<Task>();
            foreach (AbstractAnimation animation in animations)
            {
                tasks.Add(animation.StartAsync());
            }

            await Task.WhenAll(tasks);
            return animations;
        }

        public static IEnumerable<AbstractAnimation> Pause(this IEnumerable<AbstractAnimation> animations)
        {
            foreach (AbstractAnimation animation in animations)
            {
                animation.Pause();
            }

            return animations;
        }

        public static IEnumerable<AbstractAnimation> Resume(this IEnumerable<AbstractAnimation> animations)
        {
            foreach (AbstractAnimation animation in animations)
            {
                animation.Resume();
            }

            return animations;
        }

        public static IEnumerable<AbstractAnimation> Stop(this IEnumerable<AbstractAnimation> animations,
            bool triggerOnCompleted = false)
        {
            foreach (AbstractAnimation animation in animations)
            {
                animation.Stop(triggerOnCompleted);
            }

            return animations;
        }

        public static IEnumerable<AbstractAnimation> Reset(this IEnumerable<AbstractAnimation> animations)
        {
            foreach (AbstractAnimation animation in animations)
            {
                animation.Reset();
            }

            return animations;
        }

        #endregion
    }
}