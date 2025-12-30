using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Wrapper class that is used to apply motions to an array of objects of any type using a echo
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class EchoMotionProxyArray<TItem>
    {
        public EchoMotionProxyArray(Echo echo, TItem[] array)
        {
            Echo = echo;
            Array = array;
        }

        private Echo Echo { get; }
        private TItem[] Array { get; }

        /// <summary>
        /// Provides a callback used to apply all the motions needed(not echo settings because it would apply them for each member of the array).
        /// </summary>
        /// <param name="applyCallback"></param>
        public Echo Apply(Action<EchoMotionProxy<TItem>> applyCallback)
        {
            if (applyCallback == null)
            {
                throw new ArgumentException("Callback cannot be null");
            }

            for (int i = 0; i < Array.Length; i++)
            {
                applyCallback(Echo.For(Array[i]));
            }

            return Echo;
        }
    }
}
