using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IBuilder<TItem>
    {
        public TItem Build();
    }

    public interface IIdentifiableBuilder
    {
        public string DisplayName { get; }
        public bool IsEnabled { get; }
    }

    public abstract class AbstractBuilder
    {
    }

    public abstract class AbstractBuilder<TItem> : AbstractBuilder, IBuilder<TItem>
    {
        public abstract TItem Build();
    }
}