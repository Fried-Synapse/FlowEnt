using System;

namespace FriedSynapse.FlowEnt
{
    internal class SkipFramesStartHelper : AbstractStartHelper
    {
        public SkipFramesStartHelper(IUpdateController updateController, UpdateType updateType, int frames, Action<float> callback) : base(updateController, updateType, callback)
        {
            this.frames = frames;
        }

        private int frames;

        internal override void UpdateInternal(float deltaTime)
        {
            --frames;
            if (frames > 0)
            {
                return;
            }

            updateController.UnsubscribeFromUpdate(this);
            callback.Invoke(0);
        }
    }
}
