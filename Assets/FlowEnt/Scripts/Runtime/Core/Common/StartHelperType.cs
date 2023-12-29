using System;

namespace FriedSynapse.FlowEnt
{
    [Flags]
    internal enum StartHelperType
    {
        None = 0,
        SkipFrames = 1 << 0,
        Delay = 1 << 1,
        DelayUntil = 1 << 2,
    }
}