namespace FlowEnt
{
    public class FastListItem<T>
        where T : FastListItem<T>
    {
        //HACK I know these should be properties and not public fields, but heck this saves 20fps(from 80 to 100) for 128k tweens...
        internal T previous;

        internal T next;
    }

    internal class FastList<T, TAnchor>
        where T : FastListItem<T>
        where TAnchor : T, new()
    {
        internal FastList()
        {
            Anchor = new TAnchor();
            last = Anchor;
        }

        //TODO test public field
        public TAnchor Anchor { get; }
        private T last;
        internal void Add(T item)
        {
            item.previous = last;
            //HACK it's faster to reset the next in here than on remove
            if (item.next != null)
            {
                item.next = null;
            }
            last.next = item;
            last = item;
        }
        internal void Remove(T item)
        {
            T previous = item.previous;
            if (last == item)
            {
                last = previous;
            }

            //HACK we're not removing the first item ever, so no need to check if the prev item is null.
            item.previous.next = item.next;

            if (item.next != null)
            {
                item.next.previous = previous;
            }
        }

        internal void Replace(T original, T replacement)
        {
            original.previous.next = replacement;
            if (original.next != null)
            {
                original.next.previous = replacement;
            }
            replacement.next = original.next;
            replacement.previous = original.previous;
        }
    }
}
