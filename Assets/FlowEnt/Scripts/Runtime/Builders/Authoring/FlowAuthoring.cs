namespace FriedSynapse.FlowEnt
{
    public class FlowAuthoring : AbstractAuthoring<Flow, FlowBuilder>, IGizmoDrawer
    {
        void IGizmoDrawer.OnGizmosDrawing()
        {
#if UNITY_EDITOR
            ((IGizmoDrawer)AnimationBuilder).OnGizmosDrawing();
#endif
        }
    }
}