namespace FlowEnt
{
    public static class ExtensionsHelper
    {
        public static MotionWrapper<T> PrepareMotion<T>(T item, float time)
            => Flow.CreateAutoPlayable().Enqueue(time).For(item);
    }
}
