using System;

namespace FlowEnt
{
    public class FlowEntException : Exception
    {
        public Flow Flow { get; }
        public Thread Thread { get; }
        public Motion Motion { get; }

        public FlowEntException(string message) : base(message)
        {
        }

        public FlowEntException(Flow flow, string message) : this(message)
        {
            Flow = flow;
        }

        public FlowEntException(Thread thread, string message) : this(message)
        {
            Thread = thread;
        }

        public FlowEntException(Motion motion, string message) : this(message)
        {
            Motion = motion;
        }
    }
}