using System;

namespace FlowEnt
{
    public class Motion<T>
    {
        public Motion(Tween tween, T element)
        {
            Tween = tween;
            Element = element;
        }

        public Tween Tween { get; }
        public Thread Thread => Tween.Thread;
        public Flow Flow => Thread.Flow;
        public T Element { get; }

        public Tween Wrap()
            => Tween;

        public Motion<T> OnStart(Action callback)
        {
            Tween.OnStart(callback);
            return this;
        }

        public Motion<T> OnUpdate(Action<float> callback)
        {
            Tween.OnUpdate(callback);
            return this;
        }

        public Motion<T> OnComplete(Action callback)
        {
            Tween.OnComplete(callback);
            return this;
        }

        public Motion<TElement> For<TElement>(TElement element)
            => new Motion<TElement>(Tween, element);
    }
}
