using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Exception used by the FlowEnt library.
    /// </summary>
#pragma warning disable RCS1194
    public class FlowEntException : Exception
#pragma warning restore RCS1194
    {
        /// <summary>
        /// The flow attached to this exception
        /// </summary>
        public Flow Flow { get; }

        /// <summary>
        /// The tween attached to this exception
        /// </summary>
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