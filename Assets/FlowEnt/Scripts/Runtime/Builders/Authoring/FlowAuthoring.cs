namespace FriedSynapse.FlowEnt
{
    public class FlowAuthoring : AbstractAuthoring<Flow, FlowBuilder>, IGizmoDrawer
    {
#if UNITY_EDITOR
        void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
        {
            ((IGizmoDrawer)AnimationBuilder).OnGizmosDrawing(options);
        }
#endif
    }
}