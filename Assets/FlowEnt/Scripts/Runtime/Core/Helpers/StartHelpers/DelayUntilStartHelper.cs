using System;

namespace FriedSynapse.FlowEnt
{
    internal class DelayUntilStartHelper : AbstractStartHelper
    {
        public DelayUntilStartHelper(IUpdateController updateController, UpdateType updateType, Func<bool> condition, Action<float> callback) : base(updateController, updateType, callback)
        {
            this.condition = condition ?? throw new ArgumentException("Condition cannot be null", nameof( condition));
        }

        private readonly Func<bool> condition;

        internal override void UpdateInternal(float deltaTime)
        {
            if (!condition.Invoke())
            {
                return;
            }

            updateController.UnsubscribeFromUpdate(this);
            callback.Invoke(0);

        }
    }
}
