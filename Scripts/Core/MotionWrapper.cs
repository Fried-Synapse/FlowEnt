using System;

namespace FlowEnt
{
    public class MotionWrapper<T>
    {
        public MotionWrapper(Tween tween, T item)
        {
            Tween = tween;
            Item = item;
        }

        public Tween Tween { get; }
        public T Item { get; }

        public MotionWrapper<T> Apply(IMotion motion)
        {
            Tween.Apply(motion);
            return this;
        }

        public MotionWrapper<TElement> For<TElement>(TElement element)
            => new MotionWrapper<TElement>(Tween, element);
    }
}
