namespace FriedSynapse.FlowEnt
{
    internal class UpdatablesFastList<TUpdatable> : FastList<AbstractUpdatable, UpdatableAnchor>
        where TUpdatable : AbstractUpdatable
    {
    }
}
