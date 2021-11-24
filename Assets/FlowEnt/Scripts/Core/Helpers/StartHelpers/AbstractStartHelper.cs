using System;

namespace FriedSynapse.FlowEnt
{
    internal abstract class AbstractStartHelper : AbstractUpdatable
    {
        protected AbstractStartHelper(IUpdateController updateController, Action<float> callback) : base(updateController)
        {
            this.updateController.SubscribeToUpdate(this);
            this.callback = callback;
        }

        internal override void StartInternal(float deltaTime = 0)
        {
        }

        protected Action<float> callback;
    }
}
