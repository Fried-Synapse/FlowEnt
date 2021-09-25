using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Provides common options for behaviours that require frame update.
    /// </summary>
    public abstract class AbstractUpdatable : FastListItem<AbstractUpdatable>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AbstractUpdatable"/> class.
        /// </summary>
        protected AbstractUpdatable() : this(FlowEntController.Instance)
        {
        }

        private protected AbstractUpdatable(IUpdateController updateController)
        {
            Id = lastId;
            ++lastId;
            this.updateController = updateController;
        }

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

        internal IUpdateController updateController;

        #region Events

        private protected Action onStarted;
        private protected Action<float> onUpdated;
        private protected Action onCompleted;

        #endregion

        internal abstract void StartInternal(float deltaTime = 0);
        internal abstract void UpdateInternal(float deltaTime);

        /// <summary>
        /// Stops the animation.
        /// </summary>
        /// <param name="triggerOnCompleted">If set to true will trigger the "OnCompleted" event on the animation</param>
        public virtual void Stop(bool triggerOnCompleted = false)
        {
        }
    }

    internal class UpdatableAnchor : AbstractUpdatable
    {
        private const string InvalidImplementation = "This method should not be called.";
        public UpdatableAnchor() : base(0)
        {
        }

        internal override void StartInternal(float deltaTime)
        {
            throw new FlowEntException(InvalidImplementation);
        }

        internal override void UpdateInternal(float deltaTime)
        {
            throw new FlowEntException(InvalidImplementation);
        }
    }
}
