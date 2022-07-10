using System;

namespace FriedSynapse.FlowEnt
{
    internal interface IFluentEchoOptionable<TEcho> : IFluentAnimationOptionable<TEcho>
    {
        /// <summary>
        /// Sets the amount of time in seconds that this echo will last.
        /// </summary>
        /// <param name="timeout"></param>
        TEcho SetTimeout(float? timeout);

        /// <summary>
        /// Sets the condition that when true, it will stop the echo.
        /// </summary>
        /// <param name="stopCondition"></param>
        TEcho SetStopCondition(Func<float, bool> stopCondition);
    }

    internal interface IFluentEchoEventable<TEcho> : IFluentAnimationEventable<TEcho>
    {
    }
}
