using System;

namespace FlowEnt
{
    public class FlowEntObject
    {
        public FlowEntObject()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}