using System;

namespace FriedSynapse.FlowEnt
{
    internal abstract class AbstractStartHelper : AbstractUpdatable
    {
        protected AbstractStartHelper(IUpdateController updateController, UpdateType updateType, Action<float> callback) : base(updateController)
        {
            this.updateController.SubscribeToUpdate(this);
            this.updateType = updateType;
            this.callback = callback;
        }

        internal override void StartInternal(float deltaTime = 0)
        {
        }

        internal void Pause()
        {
            updateController.UnsubscribeFromUpdate(this);
        }

        internal void Resume()
        {
            updateController.SubscribeToUpdate(this);
        }

        protected Action<float> callback;
    }
}
