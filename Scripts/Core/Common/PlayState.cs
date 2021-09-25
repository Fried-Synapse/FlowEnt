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
    public enum PlayState
    {
        /// <summary>
        /// The animation is being build. The start method was not called.
        /// </summary>
        Building = 0x01,
        /// <summary>
        /// The animation has started but it's waiting for a delay.
        /// </summary>
        Waiting = 0x02,
        /// <summary>
        /// The animation is playing.
        /// </summary>
        Playing = 0x04,
        /// <summary>
        /// The animation was paused.
        /// </summary>
        Paused = 0x08,
        /// <summary>
        /// The animation completed or was stopped.
        /// </summary>
        Finished = 0x10
    }
}
