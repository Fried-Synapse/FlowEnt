namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Interface used for update controllers. There is one main update controller(FlowEntController) and each and every flow is an update controller itself
    /// </summary>
    public interface IUpdateController
    {
        public void SubscribeToUpdate(AbstractUpdatable updatable);

        public void UnsubscribeFromUpdate(AbstractUpdatable updatable);
    }
}
