using System;

namespace FriedSynapse.FlowEnt
{
#pragma warning disable RCS1194
    public class AnimationException : Exception
#pragma warning restore RCS1194
    {
        public AnimationException(AbstractAnimation animation, string message) : base(message)
        {
            Animation = animation;
        }

        /// <summary>
        /// The animation attached to this exception.
        /// </summary>
        public AbstractAnimation Animation { get; }
    }
}
