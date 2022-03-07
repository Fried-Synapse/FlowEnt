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

    [Serializable]
    public abstract class AbstractMotionBuilder<T> : AbstractBuilder<T>, IMotionBuilder
    {
#pragma warning disable IDE0044, RCS1169, RCS1213, IDE0051, CS0414
        [SerializeField]
        private string displayName;
        public string DisplayName => displayName;
        [SerializeField]
        private bool isDisplayNameEnabled;
        [SerializeField]
        private bool isEnabled = true;
        public bool IsEnabled => isEnabled;
#pragma warning restore IDE0044, RCS1169, RCS1213, IDE0051, CS0414
    }
}
