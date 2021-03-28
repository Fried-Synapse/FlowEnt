using System;

namespace FlowEnt
{
    public class MotionWrapper<T>
    {
        public MotionWrapper(Tween tween, T element)
        {
            Tween = tween;
            Element = element;
        }

        public Tween Tween { get; }
        public Thread Thread => Tween.Thread;
        public Flow Flow => Thread.Flow;
        public T Element { get; }

        public MotionWrapper<T> Apply(IMotion callback)
        {
            Tween.Apply(callback);
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
