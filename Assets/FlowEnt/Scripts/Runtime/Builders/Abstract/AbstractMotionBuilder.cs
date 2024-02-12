using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IMotionBuilder : IListBuilderItem
    {
        public DisplayName DisplayName { get; }
    }

    [Serializable]
    public abstract class AbstractMotionBuilder<T> : AbstractBuilder<T>, IMotionBuilder
    {
        [SerializeField]
        private DisplayName displayName;

        public DisplayName DisplayName => displayName;

        [SerializeField]
        private bool isEnabled = true;

        public bool IsEnabled => isEnabled;

#if UNITY_EDITOR
        [SerializeField]
        private GizmoOptions gizmoOptions;

        public GizmoOptions GizmoOptions => gizmoOptions;
#endif
    }
}