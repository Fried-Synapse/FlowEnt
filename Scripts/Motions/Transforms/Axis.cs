using System;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    [Flags]
    public enum Axis
    {
        None = 0x00,
        X = 0x01,
        Y = 0x02,
        Z = 0x04,
        XY = X | Y,
        XZ = X | Z,
        YZ = Y | Z,
        XYZ = X | Y | Z,
    }
}
