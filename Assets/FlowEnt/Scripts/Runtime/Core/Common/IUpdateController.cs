namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Interface used for update controllers. There is one main update controller(FlowEntController) and each and every flow is an update controller itself
    /// </summary>
    public interface IUpdateController
    {
        internal void SubscribeToUpdate(AbstractUpdatable updatable);

        internal void UnsubscribeFromUpdate(AbstractUpdatable updatable);
    }
}
