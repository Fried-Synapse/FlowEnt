using System.Collections.Generic;
using System;
using System.Linq;

namespace FriedSynapse.Quickit
{
    public abstract class AbstractObjectPool<T>
    {
        protected class PoolModel
        {
            public T Object { get; }
            public bool IsUsed { get; set; }

            public PoolModel(T obj)
            {
                Object = obj;
            }
        }

        protected AbstractObjectPool(Action<T> onObjectAdded = null)
        {
            Items = new List<PoolModel>();
            OnObjectAdded += onObjectAdded;
        }

        protected List<PoolModel> Items { get; }
        protected Action<T> OnObjectAdded { get; }
        public int Size => Items.Count;

        protected List<PoolModel> GetUsedItems() => Items.FindAll(m => m.IsUsed);
        protected List<PoolModel> GetAvailableItems() => Items.FindAll(m => !m.IsUsed);
        public int GetUsedCount() => Items.Count(m => m.IsUsed);
        public int GetAvailableCount() => Items.Count(m => !m.IsUsed);

        protected abstract T CreateObject();

        public virtual void Increase(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                T obj = CreateObject();
                OnObjectAdded?.Invoke(obj);
                PoolModel model = new PoolModel(obj);
                Items.Add(model);
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

            List<PoolModel> available = GetAvailableItems();
            int index = 0;
            while (index < amount)
            {
                Items.Remove(available[index++]);
            }
        }

        public virtual T Get()
        {
            PoolModel found = Items.Find(m => !m.IsUsed);
            if (found == null)
            {
                Increase(1);
                found = Items.Last();
            }
            found.IsUsed = true;
            return found.Object;
        }

        public virtual void Use(T obj)
        {
            PoolModel model = Items.Find(m => m.Object.Equals(obj));
            if (model != null)
            {
                model.IsUsed = true;
            }
        }

        public virtual bool IsUsed(T obj)
            => Items.Find(m => m.Object.Equals(obj))?.IsUsed == true;

        public virtual void Revoke(T obj)
        {
            PoolModel model = Items.Find(m => m.Object.Equals(obj));
            if (model != null)
            {
                model.IsUsed = false;
            }
        }

        public virtual void RevokeAll()
        {
            foreach (PoolModel model in Items)
            {
                model.IsUsed = false;
            }
        }
    }
}