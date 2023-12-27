using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IListBuilderItem
    {
    }

    [Serializable]
    public abstract class AbstractListBuilder<TBuilderItem, TBuiltItem> : AbstractBuilder<List<TBuiltItem>>
        where TBuilderItem : IListBuilderItem
    {
        [SerializeReference]
        protected List<TBuilderItem> items = new List<TBuilderItem>();

        public List<TBuilderItem> Items => items;
    }
}