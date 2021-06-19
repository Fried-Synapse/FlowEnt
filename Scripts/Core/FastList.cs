namespace FlowEnt
{
    internal interface IFastListItem
    {
        public int Index { get; set; }
    }

    internal abstract class AbstractFastListItem : IFastListItem
    {
        public int Index { get; set; }
    }

    internal class FastList<T>
        where T : IFastListItem
    {
        public FastList(int capacity)
        {
            array = new T[capacity];
        }

        public FastList(T[] array)
        {
            this.array = array;
            count = array.Length;
        }

        private T[] array;

        private int count;
        public int Count => count;

        public T this[int i]
        {
            get
            {
                return array[i];
            }
            set
            {
                array[i] = value;
            }
        }

        public void Add(T item)
        {
            array[count] = item;
            item.Index = count;
            count++;
        }

        public T Last()
            => array[count - 1];

        public void Remove(T item)
            => RemoveAt(item.Index);

        public void RemoveAt(int index)
        {
            T lastItem = array[count - 1];
            array[index] = lastItem;
            lastItem.Index = index;
            RemoveLast();
        }

        public void RemoveLast()
        {
            array[count - 1] = default(T);
            count--;
        }
    }
}
