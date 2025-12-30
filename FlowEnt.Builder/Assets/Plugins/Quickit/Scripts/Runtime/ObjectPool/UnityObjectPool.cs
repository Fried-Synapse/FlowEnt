using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace FriedSynapse.Quickit
{
    public class UnityObjectPool<T> : AbstractObjectPool<T>
        where T : Object
    {
        public class Options
        {
            public T Template { get; set; }
            public int StartAmount { get; set; }
            public Transform Parent { get; set; }
            public bool ReuseTemplate { get; set; }
            public Action<T> OnObjectAdded { get; set; }
        }

        public UnityObjectPool(T template, int startAmount, Transform parent = null, bool reuseTemplate = false, Action<T> onObjectAdded = null)
            : base(onObjectAdded)
        {
            Template = template;
            Parent = parent;

            if (reuseTemplate)
            {
                PoolModel model = new PoolModel();
                items.Add(template, model);
                onItemAdded?.Invoke(template);
            }
            Increase(reuseTemplate ? startAmount - 1 : startAmount);
        }

        public UnityObjectPool(Options options)
            : this(options.Template, options.StartAmount, options.Parent, options.ReuseTemplate, options.OnObjectAdded)
        {
        }

        protected T Template { get; }
        private Transform Parent { get; }

        protected override T CreateObject()
        {
            return Object.Instantiate(Template, Parent);
        }
    }
}