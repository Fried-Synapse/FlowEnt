using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
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
