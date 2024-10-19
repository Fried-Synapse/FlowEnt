using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides common options for behaviours that require frame update.
    /// </summary>
    public abstract class AbstractUpdatable : FastListItem<AbstractUpdatable>
    {
        /// <summary>
        /// Creates a new instance using <see cref="FlowEntController"/>.
        /// </summary>
        protected AbstractUpdatable() : this(FlowEntController.Instance)
        {
        }

        /// <summary>
        /// Creates a new instance using the specified <see cref="IUpdateController"/>.
        /// </summary>
        /// <param name="updateController"></param>
        private protected AbstractUpdatable(IUpdateController updateController)
        {
            Id = lastId;
            ++lastId;
            this.updateController = updateController;
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
            stackTrace = FlowEntDebug.GetStackTrace();
#endif
        }

#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
        internal string stackTrace;
        internal string hierarchy;
        internal T SetHierarchy<T>(string hierarchy)
            where T : AbstractUpdatable
        {
            this.hierarchy = hierarchy;
            return (T)this;
        }
#endif

        //HACK for having a constructor that does nothing, to instantiate quicker when needed
        private protected AbstractUpdatable(int thisIsAnEmptyConstructorForAnchor)
        {
        }

        private static ulong lastId;

        /// <summary>
        /// A value used to identify the current updatable that is automatically assigned.
        /// </summary>
        /// <remarks>
        /// The value is generated and it automatically increments starting from 0.
        /// </remarks>
        public ulong Id { get; }

        /// <summary>
        /// A name that can be used to identify the animation. Empty by default.
        /// </summary>
        public string Name { get; set; }

        internal UpdateType updateType;

        internal IUpdateController updateController;

        #region Events

        protected Action onStarting;
        private protected Action onStarted;
        protected Action<float> onUpdating;
        private protected Action<float> onUpdated;
        protected Action onCompleting;
        private protected Action onCompleted;

        #endregion

        internal abstract void StartInternal(float deltaTime = 0);
        internal abstract void UpdateInternal(float deltaTime);

        /// <summary>
        /// Stops the animation.
        /// </summary>
        /// <param name="triggerOnCompleted">If set to true will trigger the "OnCompleted" event on the animation</param>
        public void Stop(bool triggerOnCompleted = false)
        {
            StopInternal(triggerOnCompleted);
        }

        protected virtual void StopInternal(bool triggerOnCompleted)
        {
        }

        /// <summary>
        /// Resets the animation.
        /// </summary>
        public void Reset()
        {
            ResetInternal();
        }

        protected virtual void ResetInternal()
        {
        }

        public override string ToString()
            => $"{GetType().Name} " +
#if UNITY_EDITOR
               $"[Id: {(Application.isPlaying ? Id.ToString() : "-")}" +
#else
               $"[Id: {Id}" +
#endif
               $"{(string.IsNullOrEmpty(Name) ? string.Empty : $", Name: \"{Name}\"")}]";
    }

    internal class UpdatableAnchor : AbstractUpdatable
    {
        public UpdatableAnchor() : base(0)
        {
        }

        internal override void StartInternal(float deltaTime)
        {
            throw new InvalidOperationException(FlowEntInternalConstants.InvalidImplementation);
        }

        internal override void UpdateInternal(float deltaTime)
        {
            throw new InvalidOperationException(FlowEntInternalConstants.InvalidImplementation);
        }
    }
}