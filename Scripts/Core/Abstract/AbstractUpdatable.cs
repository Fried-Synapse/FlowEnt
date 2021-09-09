using System;

namespace FriedSynapse.FlowEnt
{
    public abstract class AbstractUpdatable : FastListItem<AbstractUpdatable>
    {
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
        public ulong Id { get; }

        internal IUpdateController updateController;

        #region Events

        private protected Action onStarted;
        private protected Action<float> onUpdated;
        private protected Action onCompleted;

        #endregion

        internal abstract void StartInternal(float deltaTime = 0);
        internal abstract void UpdateInternal(float deltaTime);
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
