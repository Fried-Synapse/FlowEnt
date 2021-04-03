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
        public Thread Thread => Tween.Thread;
        public Flow Flow => Thread.Flow;
        public T Item { get; }

        public MotionWrapper<T> Apply(IMotion motion)
        {
            Tween.Apply(motion);
            return this;
        }

        public MotionWrapper<T> OnStart(Action callback)
        {
            Tween.OnStart(callback);
            return this;
        }

        public MotionWrapper<T> OnUpdate(Action<float> callback)
        {
            Tween.OnUpdate(callback);
            return this;
        }

        public MotionWrapper<T> OnComplete(Action callback)
        {
            Tween.OnComplete(callback);
            return this;
        }

        public MotionWrapper<TElement> For<TElement>(TElement element)
            => new MotionWrapper<TElement>(Tween, element);
    }
}
