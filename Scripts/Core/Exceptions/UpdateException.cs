using System;

namespace FriedSynapse.FlowEnt
{
#pragma warning disable RCS1194
    public class UpdateException : Exception
#pragma warning restore RCS1194
    {
        public UpdateException(string origin, string message, Exception innerException) : base(message, innerException)
        {
            Origin = origin;
        }

        /// <summary>
        /// The origin of the animation which created this error.
        /// </summary>
        public string Origin { get; }

        public override string Message => $"{base.Message}\n\nHappened while running animation created in: {Origin}";
    }
}