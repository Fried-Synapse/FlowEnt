namespace FlowEnt
{
    public abstract class AbstractUpdatable : FlowEntObject
    {
        private AbstractUpdatable previous;
        private AbstractUpdatable next;

        internal AbstractUpdatable Previous { get => previous; set => previous = value; }
        internal AbstractUpdatable Next { get => next; set => next = value; }

        internal abstract float? UpdateInternal(float deltaTime);
    }

    internal class StartUpdatable : AbstractUpdatable
    {
        internal override float? UpdateInternal(float deltaTime)
        {
            return null;
        }
    }

    internal class FasterList
    {
        public FasterList()
        {
            first = new StartUpdatable();
            index = first;
        }

        private AbstractUpdatable first;
        public AbstractUpdatable First => first;
        private AbstractUpdatable index;

        internal void Add(AbstractUpdatable updatable)
        {
            index.Next = updatable;
            updatable.Previous = index;
            index = updatable;
        }
        internal void Remove(AbstractUpdatable updatable)
        {
            AbstractUpdatable previous = updatable.Previous;
            if (index == updatable)
            {
                index = previous;
            }
            if (updatable.Previous != null)
            {
                updatable.Previous.Next = updatable.Next;
            }
            if (updatable.Next != null)
            {
                updatable.Next.Previous = previous;
            }
        }
    }
}
