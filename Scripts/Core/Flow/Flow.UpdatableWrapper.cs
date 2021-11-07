using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow
    {
        private enum UpdatableWrapperType
        {
            Updatable,
            UpdatableGetter
        }

        private class UpdatableWrapper
        {
            private UpdatableWrapper(int index, float? timeIndex = null)
            {
                this.index = index;
                this.timeIndex = timeIndex;
            }
            public UpdatableWrapper(AbstractUpdatable updatable, int index, float? timeIndex = null) : this(index, timeIndex)
            {
                type = UpdatableWrapperType.Updatable;
                this.updatable = updatable;
            }

            public UpdatableWrapper(Func<AbstractUpdatable> updatableGetter, int index, float? timeIndex = null) : this(index, timeIndex)
            {
                type = UpdatableWrapperType.UpdatableGetter;
                this.updatableGetter = updatableGetter;
            }

            private readonly UpdatableWrapperType type;
            private readonly AbstractUpdatable updatable;
            private readonly Func<AbstractUpdatable> updatableGetter;
            public readonly int index;
            public float? timeIndex;
            public UpdatableWrapper next;

            public AbstractUpdatable GetUpdatable()
            {
                switch (type)
                {
                    case UpdatableWrapperType.Updatable:
                        return updatable;
                    case UpdatableWrapperType.UpdatableGetter:
                        return updatableGetter.Invoke();
                    default:
                        throw new ArgumentException("Unknown updatable type found.");
                }
            }
        }
    }
}
