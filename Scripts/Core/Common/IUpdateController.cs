namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Interface used for update controllers. There is one main update controller(FlowEntController) and each and every flow is an update controller itself
    /// </summary>
    internal interface IUpdateController
    {
        void SubscribeToUpdate(AbstractUpdatable updatable);

        void UnsubscribeFromUpdate(AbstractUpdatable updatable);
    }
}
