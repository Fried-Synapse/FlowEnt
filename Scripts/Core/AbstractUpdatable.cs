namespace FlowEnt
{
    public abstract class AbstractUpdatable : FlowEntObject, IFastListItem
    {
        int IFastListItem.Index { get; set; }

        internal abstract float? UpdateInternal(float deltaTime);
    }
}
