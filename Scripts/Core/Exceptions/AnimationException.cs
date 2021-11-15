using System;

namespace FriedSynapse.FlowEnt
{
#pragma warning disable RCS1194
    public class AnimationException : Exception
#pragma warning restore RCS1194
    {
        public AnimationException(string message) : base(message)
        {
        }
    }
}
