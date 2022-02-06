using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Wrapper class that is used to apply motions to an array of objects of any type using a tween
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class TweenMotionProxyArray<TItem>
        where TItem : class
    {
        public TweenMotionProxyArray(Tween tween, TItem[] array)
        {
            Tween = tween;
            Array = array;
        }

        private Tween Tween { get; }
        private TItem[] Array { get; }

        /// <summary>
        /// Provides a callback used to apply all the motions needed(not tween settings because it would apply them for each member of the array).
        /// </summary>
        /// <param name="applyCallback"></param>
        public Tween Apply(Action<TweenMotionProxy<TItem>> applyCallback)
        {
            if (applyCallback == null)
            {
                throw new ArgumentException("Callback cannot be null");
            }

            for (int i = 0; i < Array.Length; i++)
            {
                applyCallback(Tween.For(Array[i]));
            }

            return Tween;
        }
    }
}
