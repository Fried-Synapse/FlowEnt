using System;

namespace FriedSynapse.FlowEnt
{
    internal sealed class EmptyUpdatable : AbstractUpdatable
    {
        internal override void StartInternal(float deltaTime = 0)
        {
            if (updateController is Flow parentFlow)
            {
                parentFlow.CompleteUpdatable(this);
            }
        }

        internal override void UpdateInternal(float deltaTime)
        {
            throw new InvalidOperationException("This method should not be called");
        }
    }
}
