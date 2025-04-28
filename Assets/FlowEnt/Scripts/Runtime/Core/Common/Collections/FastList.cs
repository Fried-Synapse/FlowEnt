using System;
using System.Collections;
using System.Collections.Generic;

namespace FriedSynapse.FlowEnt
{
    internal class FastList<T> : IEnumerable<T>
    {
        private T[] items;
        private int size;
        public int Count => size;

        public T this[int index] { get => items[index]; set => items[index] = value; }

        public FastList(int capacity)
        {
            items = new T[capacity];
            size = 0;
        }

        public void Add(T item)
        {
            if (size >= items.Length)
            {
                Grow();
            }

            items[size++] = item;
        }

        public void AddRange(IEnumerable<T> items)
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

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < size; i++)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}