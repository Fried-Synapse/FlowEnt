using System;

namespace FriedSynapse.FlowEnt
{
    //TODO add tests and docs
    public class TweenMotionArray<T>
    {
        public TweenMotionArray(Tween tween, T[] array)
        {
            Tween = tween;
            Array = array;
        }

        private Tween Tween { get; }
        private T[] Array { get; }

        public Tween Apply(Action<TweenMotion<T>> applyCallback)
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
