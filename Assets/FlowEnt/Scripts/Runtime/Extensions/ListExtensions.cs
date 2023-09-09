using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriedSynapse.FlowEnt
{
    public static class ListExtensions
    {
        public static IEnumerable<AbstractAnimation> Build(
            this IEnumerable<IAbstractAnimationBuilder> animationsBuilders)
            => animationsBuilders.Select(animationBuilder => animationBuilder.Build());

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

        public static IEnumerable<AbstractAnimation> Stop(this IEnumerable<AbstractAnimation> animations)
        {
            foreach (AbstractAnimation animation in animations)
            {
                animation.Stop();
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
    }
}