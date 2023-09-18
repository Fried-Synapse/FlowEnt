using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IMotionBuilder : IIdentifiableBuilder, IListBuilderItem
    {
    }

    [Serializable]
    public abstract class AbstractMotionBuilder<T> : AbstractBuilder<T>, IMotionBuilder
    {
        [SerializeField]
        private string displayName;

        public string DisplayName => displayName;

        [SerializeField]
        private bool isDisplayNameEnabled;

        [SerializeField]
        private bool isEnabled = true;

        public bool IsEnabled => isEnabled;
    }
}