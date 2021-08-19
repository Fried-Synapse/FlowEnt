namespace FriedSynapse.FlowEnt
{
    internal interface IUpdateController
    {
        void SubscribeToUpdate(AbstractUpdatable updatable);

        void UnsubscribeFromUpdate(AbstractUpdatable updatable);
    }
}
