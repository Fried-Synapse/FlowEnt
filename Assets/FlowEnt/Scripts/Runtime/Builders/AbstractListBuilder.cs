using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractListBuilder<TBuilderItem, TBuiltItem> : AbstractBuilder<List<TBuiltItem>>
        where TBuilderItem : IBuilder
    {
        [SerializeReference]
        protected List<TBuilderItem> items = new List<TBuilderItem>();

        public List<TBuilderItem> Items => items;
    }
}