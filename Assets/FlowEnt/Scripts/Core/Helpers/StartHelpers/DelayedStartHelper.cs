using System;

namespace FriedSynapse.FlowEnt
{
    internal class DelayedStartHelper : AbstractStartHelper
    {
        public DelayedStartHelper(IUpdateController updateController, float time, Action<float> callback) : base(updateController, callback)
        {
            this.time = time;
        }

        private float time;

        internal override void UpdateInternal(float deltaTime)
        {
            time -= deltaTime;
            if (time >= 0f)
            {
                return;
            }

            updateController.UnsubscribeFromUpdate(this);
            callback.Invoke(-time);
        }
    }
}
