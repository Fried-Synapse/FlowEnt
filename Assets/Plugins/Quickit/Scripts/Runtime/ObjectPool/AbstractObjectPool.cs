using System.Collections.Generic;
using System;
using System.Linq;

namespace FriedSynapse.Quickit
{
    public abstract class AbstractObjectPool<T>
    {
        protected class PoolModel
        {
            public bool isUsed;
        }

        protected AbstractObjectPool(Action<T> onItemAdded = null)
        {
            this.onItemAdded += onItemAdded;
        }

        protected Dictionary<T, PoolModel> items = new Dictionary<T, PoolModel>();
        protected Action<T> onItemAdded;
        public int Size => items.Count;

        protected List<T> GetUsedItems() => items.Where(kvp => kvp.Value.isUsed).Select(kvp => kvp.Key).ToList();
        protected List<T> GetAvailableItems() => items.Where(kvp => !kvp.Value.isUsed).Select(kvp => kvp.Key).ToList();
        public int GetUsedCount() => items.Count(kvp => kvp.Value.isUsed);
        public int GetAvailableCount() => items.Count(kvp => !kvp.Value.isUsed);

        protected abstract T CreateObject();

        public virtual void Increase(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                T item = CreateObject();
                PoolModel model = new PoolModel();
                items.Add(item, model);
                onItemAdded?.Invoke(item);
            }
        }

        public virtual void Decrease(int amount)
        {
            int availableCount = GetAvailableCount();
            if (amount > availableCount)
            {
                Logger.LogWarning(LogCategory.ObjectPool, $"Trying to decrease by {amount} but only {availableCount} objects are available to be removed. Decreasing by {availableCount}.");
                amount = availableCount;
            }

            List<T> available = GetAvailableItems();
            int index = 0;
            while (index < amount)
            {
                items.Remove(available[index++]);
            }
        }

        public virtual T Get()
        {
            KeyValuePair<T, PoolModel> found = items.FirstOrDefault(kvp => !kvp.Value.isUsed);
            if (found.Key == null)
            {
                Increase(1);
                found = items.Last();
            }
            found.Value.isUsed = true;
            return found.Key;
        }

        public virtual void Use(T item)
        {
            if (items.TryGetValue(item, out PoolModel model))
            {
                model.isUsed = true;
            }
        }

        public virtual bool IsUsed(T item)
            => items.TryGetValue(item, out PoolModel model) && model.isUsed;

        public virtual void Revoke(T item)
        {
            if (items.TryGetValue(item, out PoolModel model))
            {
                model.isUsed = false;
            }
        }

        public virtual void Revoke(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Revoke(item);
            }
        }

        public virtual void RevokeAll()
        {
            foreach (KeyValuePair<T, PoolModel> kvp in items)
            {
                kvp.Value.isUsed = false;
            }
        }
    }
}