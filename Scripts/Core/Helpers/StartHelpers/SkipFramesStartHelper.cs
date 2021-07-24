using System;

namespace FlowEnt
{
    internal class SkipFramesStartHelper : AbstractStartHelper
    {
        public SkipFramesStartHelper(int frames, Action<float> callback) : base(callback)
        {
            this.frames = frames;
        }

        private int frames;

        internal override float? UpdateInternal(float deltaTime)
        {
            --frames;
            if (frames > 0)
            {
                return null;
            }

            FlowEntController.Instance.UnsubscribeFromUpdate(this);
            Callback.Invoke(deltaTime);
            return null;
        }
    }
}
