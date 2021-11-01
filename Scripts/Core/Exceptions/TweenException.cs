using System;

namespace FriedSynapse.FlowEnt
{
#pragma warning disable RCS1194
    public class TweenException : Exception
#pragma warning restore RCS1194
    {
        public TweenException(Tween tween, string message) : base(message)
        {
            Tween = tween;
        }

        /// <summary>
        /// The tween attached to this exception.
        /// </summary>
        public Tween Tween { get; }

    }
}