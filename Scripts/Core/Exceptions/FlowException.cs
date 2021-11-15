using System;

namespace FriedSynapse.FlowEnt
{
#pragma warning disable RCS1194
    public class FlowException : AnimationException
#pragma warning restore RCS1194
    {
        public FlowException(Flow flow, string message) : base(message)
        {
            Flow = flow;
        }

        /// <summary>
        /// The flow attached to this exception.
        /// </summary>
        public Flow Flow { get; }
    }
}