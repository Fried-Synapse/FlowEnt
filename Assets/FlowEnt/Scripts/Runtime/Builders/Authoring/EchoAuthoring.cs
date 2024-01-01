namespace FriedSynapse.FlowEnt
{
    public class EchoAuthoring : AbstractAuthoring<Echo, EchoBuilder>, IGizmoDrawer
    {
        void IGizmoDrawer.OnGizmosDrawing()
        {
#if UNITY_EDITOR
            ((IGizmoDrawer)AnimationBuilder).OnGizmosDrawing();
#endif
        }
    }
}