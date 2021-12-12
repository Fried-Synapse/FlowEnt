using System;

namespace FriedSynapse.Quickit
{
    [Flags]
    internal enum LogLevel
    {
        None = 0x00,
        Message = 0x01,
        Warning = 0x02,
        Error = 0x04,
        Issues = Warning | Error,
        Verbose = Message | Warning | Error,
    }
}
