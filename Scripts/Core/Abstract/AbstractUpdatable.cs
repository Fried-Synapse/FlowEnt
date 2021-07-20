using System;

namespace FlowEnt
{
    public abstract class AbstractUpdatable : FastListItem<AbstractUpdatable>
    {
        protected AbstractUpdatable()
        {
            //TODO check fastest approach
            Id = lastId;
            ++lastId;
        }

        private static ulong lastId;

        public ulong Id { get; set; }

        internal abstract float? UpdateInternal(float deltaTime);
    }
}
