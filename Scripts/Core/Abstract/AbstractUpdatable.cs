using System;

namespace FlowEnt
{
    public abstract class AbstractUpdatable : FastListItem<AbstractUpdatable>
    {
        protected AbstractUpdatable()
        {
            Id = lastId;
            ++lastId;
        }

        private static ulong lastId;

        public ulong Id { get; }

        internal abstract float? UpdateInternal(float deltaTime);
    }
}
