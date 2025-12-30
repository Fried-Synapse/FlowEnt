namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Type of loop.
    /// </summary>
    public enum LoopType
    {
        /// <summary>
        /// It will reset to 0 once the value 1 is reached.
        /// </summary>
        Reset,
        /// <summary>
        /// When value 1 is reached it will tween back to 0 in the same amount of time it tweened from 0 to 1.
        /// </summary>
        PingPong
    }
}
