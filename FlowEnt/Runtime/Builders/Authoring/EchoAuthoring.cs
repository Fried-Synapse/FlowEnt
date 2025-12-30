namespace FriedSynapse.FlowEnt
{
    public class EchoAuthoring : AbstractAuthoring<Echo, EchoBuilder>, IGizmoDrawer
    {
#if UNITY_EDITOR
        void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
        {
            ((IGizmoDrawer)AnimationBuilder).OnGizmosDrawing(options);
        }
#endif
    }
}