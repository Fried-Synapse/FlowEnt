using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractMotionBuilder<T> : AbstractBuilder<T>, IIdentifiableBuilder
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
