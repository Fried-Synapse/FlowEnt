using System;

namespace FriedSynapse.FlowEnt
{
    [Flags]
    public enum PlayState
    {
        Building = 0x01,
        Waiting = 0x02,
        Playing = 0x04,
        Paused = 0x08,
        Finished = 0x10
    }
}
