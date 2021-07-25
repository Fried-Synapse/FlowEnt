using System;

namespace FlowEnt
{
    internal class AutoStartHelper : AbstractStartHelper
    {
        public AutoStartHelper(IUpdateController updateController, Action<float> callback) : base(updateController, callback)
        {
        }

        internal override void UpdateInternal(float deltaTime)
        {
            updateController.UnsubscribeFromUpdate(this);
            callback.Invoke(deltaTime);
        }
    }
}
