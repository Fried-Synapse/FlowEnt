using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Enum used to set the state of an animation.
    /// </summary>
    /// <remarks>
    /// The reason this enum is a flag, is to provide easier checking if you want to check for a selection of states for a set of animations.
    /// </remarks>
    [Flags]
#pragma warning disable RCS1135
    public enum PlayState
#pragma warning restore RCS1135
    {
        /// <summary>
        /// The animation is being build. The start method was not called.
        /// </summary>
        Building = 1 << 0,
        /// <summary>
        /// The animation has started but it's waiting for a delay.
        /// </summary>
        Waiting = 1 << 1,
        /// <summary>
        /// The animation is playing.
        /// </summary>
        Playing = 1 << 2,
        /// <summary>
        /// The animation was paused.
        /// </summary>
        Paused = 1 << 3,
        /// <summary>
        /// The animation completed or was stopped.
        /// </summary>
        Finished = 1 << 4
    }
}
