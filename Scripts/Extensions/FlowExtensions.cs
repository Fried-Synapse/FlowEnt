namespace FlowEnt
{
    public static class FlowExtensions
    {
        public static MotionWrapper<T> CreateFlowMotion<T>(this T item, float time)
            => Flow.CreateAutoPlayable().Enqueue(time).For(item);
    }
}
