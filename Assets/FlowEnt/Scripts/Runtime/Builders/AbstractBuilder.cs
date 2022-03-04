using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IBuilder<T>
    {
        public T Build();
    }

    public abstract class AbstractBuilder<T> : IBuilder<T>
    {
        public abstract T Build();
    }

    public interface IMotionBuilder
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
