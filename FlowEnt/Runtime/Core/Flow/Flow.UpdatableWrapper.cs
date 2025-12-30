using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow
    {
        private abstract class AbstractUpdatableWrapper
        {
            public AbstractUpdatableWrapper next;
            public abstract AbstractUpdatable GetUpdatable();
        }

        private class UpdatableWrapperDirect : AbstractUpdatableWrapper
        {
            public UpdatableWrapperDirect(AbstractUpdatable updatable)
            {
                this.updatable = updatable;
            }

            private readonly AbstractUpdatable updatable;
            public override AbstractUpdatable GetUpdatable() => updatable;
        }

        private class UpdatableWrapperCallback : AbstractUpdatableWrapper
        {
            public UpdatableWrapperCallback(Func<AbstractUpdatable> updatableGetter)
            {
                this.updatableGetter = updatableGetter;
            }

            private readonly Func<AbstractUpdatable> updatableGetter;
            public override AbstractUpdatable GetUpdatable() => updatableGetter.Invoke();
        }
    }
}
