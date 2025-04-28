using System.Collections.Generic;

namespace FriedSynapse.FlowEnt
{
    public class FastLinkedListNode<T>
        where T : FastLinkedListNode<T>
    {
        internal T previous;
        internal T next;
    }
    
    internal class UpdatablesFastLinkedList 
    {
        internal UpdatablesFastLinkedList()
        {
            anchor = new();
            last = anchor;
        }

        internal readonly UpdatableAnchor anchor;
        private AbstractUpdatable last;

        internal void Add(AbstractUpdatable item)
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
        internal void Remove(AbstractUpdatable item)
        {
            AbstractUpdatable previous = item.previous;
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

        internal void Replace(AbstractUpdatable original, AbstractUpdatable replacement)
        {
            original.previous.next = replacement;
            if (original.next != null)
            {
                original.next.previous = replacement;
            }
            replacement.next = original.next;
            replacement.previous = original.previous;
        }

        internal void Clear()
        {
            anchor.next = null;
            last = anchor;
        }

        internal List<AbstractUpdatable> AbstractUpdatableList()
        {
            AbstractUpdatable index = anchor.next;
            List<AbstractUpdatable> result = new();
            while (index != null)
            {
                result.Add(index);
                index = index.next;
            }

            return result;
        }
    }
}
