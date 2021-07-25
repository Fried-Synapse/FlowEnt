using System;

namespace FlowEnt
{
    internal class SkipFramesStartHelper : AbstractStartHelper
    {
        public SkipFramesStartHelper(IUpdateController updateController, int frames, Action<float> callback) : base(updateController, callback)
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
            callback.Invoke(deltaTime);
        }
    }
}
