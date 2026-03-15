using System;

namespace FriedSynapse.FlowEnt
{
    internal class AutoStartHelper : AbstractStartHelper
    {
        public AutoStartHelper(IUpdateController updateController, UpdateType updateType, Action<float> callback) : base(updateController, updateType, callback)
        {
        }

        internal override void UpdateInternal(float deltaTime)
        {
            updateController.UnsubscribeFromUpdate(this);
            callback.Invoke(deltaTime);
        }
    }
}
