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

        internal abstract void UpdateInternal(float deltaTime);
    }

    internal class UpdatableAnchor : AbstractUpdatable
    {
        public UpdatableAnchor() : base(0)
        {

        }

        internal override void UpdateInternal(float deltaTime)
        {
            throw new FlowEntException("this method should not be called");
        }
    }
}
