using System;

namespace FlowEnt
{
    internal class AutoStartHelper : AbstractStartHelper
    {
        public AutoStartHelper(Action<float> callback) : base (callback)
        {
        }

        internal override float? UpdateInternal(float deltaTime)
        {
            FlowEntController.Instance.UnsubscribeFromUpdate(this);
            Callback.Invoke(deltaTime);
            return null;
        }
    }
}
