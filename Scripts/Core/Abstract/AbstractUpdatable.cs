using System;

namespace FlowEnt
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

        private static ulong lastId;
        public ulong Id { get; }

        private protected IUpdateController updateController;
        internal IUpdateController UpdateController
        {
            get => updateController;
            set => updateController = value;
        }

        internal abstract void UpdateInternal(float deltaTime);
    }

    internal class UpdatableAnchor : AbstractUpdatable
    {
        public UpdatableAnchor() : base(null)
        {

        }

        internal override void UpdateInternal(float deltaTime)
        {
            throw new FlowEntException("this method should not be called");
        }
    }
}
