using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class AbstractListBuilder<TListItem>
        where TListItem : IBuilderListItem
    {
#pragma warning disable IDE0044, RCS1169
        [SerializeReference]
        protected List<TListItem> items = new List<TListItem>();
        public List<TListItem> Items => items;
#pragma warning restore IDE0044, RCS1169
    }
}
