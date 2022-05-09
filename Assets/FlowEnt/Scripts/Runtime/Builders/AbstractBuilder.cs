using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IBuilder<TItem>
    {
        public TItem Build();
    }

    public abstract class AbstractBuilder<TItem> : IBuilder<TItem>
    {
        public abstract TItem Build();
    }

    public interface IBuilderListItem
    {
    }

    public interface IMotionBuilder : IBuilderListItem
    {
        public string DisplayName { get; }
        public bool IsEnabled { get; }
    }
}
