using System;

namespace FlowEnt
{
    internal class DelayedStartHelper : AbstractStartHelper
    {
        public DelayedStartHelper(float time, Action<float> callback) : base(callback)
        {
            this.time = time;
        }

        private float time;

        internal override float? UpdateInternal(float deltaTime)
        {
            time -= deltaTime;
            if (time >= 0f)
            {
                return null;
            }

            FlowEntController.Instance.UnsubscribeFromUpdate(this);
            Callback.Invoke(-time);
            return null;
        }
    }
}
