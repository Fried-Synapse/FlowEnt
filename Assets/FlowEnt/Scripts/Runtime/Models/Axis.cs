using System;

namespace FriedSynapse.FlowEnt
{
    [Flags]
    public enum Axis
    {
        None = 0x00,
        X = 0x01,
        Y = 0x02,
        XY = X | Y,
        Z = 0x04,
        XZ = X | Z,
        YZ = Y | Z,
        XYZ = X | Y | Z,
    }
}
