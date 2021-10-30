using System;

namespace FriedSynapse.FlowEnt
{
    public partial class Flow
    {
        private class UpdatableWrapper
        {
            public UpdatableWrapper(object updatableObject, int index, float? timeIndex = null)
            {
                this.updatableObject = updatableObject;
                this.index = index;
                this.timeIndex = timeIndex;
            }

            private readonly object updatableObject;
            public int index;
            public float? timeIndex;
            public UpdatableWrapper next;

            public AbstractUpdatable GetUpdatable()
            {
                switch (updatableObject)
                {
                    case AbstractUpdatable abstractUpdatable:
                        return abstractUpdatable;
                    case Func<AbstractUpdatable> getAbstractUpdatable:
                        return getAbstractUpdatable.Invoke();
                    default:
                        throw new ArgumentException("Unknown updatable type found.");
                }
            }
        }
    }
}
