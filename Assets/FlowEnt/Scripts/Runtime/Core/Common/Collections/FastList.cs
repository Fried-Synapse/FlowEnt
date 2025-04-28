using System;
using System.Collections;
using System.Collections.Generic;

namespace FriedSynapse.FlowEnt
{
    internal class FastList<T> : IEnumerable<T>
    {
        private T[] items;
        private int size;
        internal int Count => size;

        internal T this[int index] { get => items[index]; set => items[index] = value; }

        internal FastList(int capacity)
        {
            items = new T[capacity];
        }

        internal void Add(T item)
        {
            if (size >= items.Length)
            {
                Grow();
            }

            items[size++] = item;
        }

        internal void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Add(item);
            }
        }

        private void Grow()
        {
            int newCapacity = size * 2;
            Array.Resize(ref items, newCapacity);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            for (int i = 0; i < size; i++)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<T>)this).GetEnumerator();
    }
}