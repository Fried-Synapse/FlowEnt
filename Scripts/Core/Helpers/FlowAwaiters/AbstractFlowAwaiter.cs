namespace FriedSynapse.FlowEnt
{
    public abstract class AbstractFlowAwaiter : AbstractUpdatable
    {
        internal override void StartInternal(float deltaTime = 0)
        {
            updateController.SubscribeToUpdate(this);
        }
    }
}
