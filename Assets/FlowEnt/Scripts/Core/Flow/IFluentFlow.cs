using System;

namespace FriedSynapse.FlowEnt
{
    internal interface IFluentFlowOptionable<T>
    {
    }

    internal interface IFluentFlowEventable<T> : IFluentAnimationEventable<T>
    {
    }
}
