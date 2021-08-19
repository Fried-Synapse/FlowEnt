using System;

namespace FriedSynapse.FlowEnt
{
    public class FlowEntException : Exception
    {
        public Flow Flow { get; }
        public Tween Tween { get; }

        public FlowEntException(string message) : base(message)
        {
        }

        public FlowEntException(Flow flow, string message) : this(message)
        {
            Flow = flow;
        }

        public FlowEntException(Tween tween, string message) : this(message)
        {
            Tween = tween;
        }
    }
}