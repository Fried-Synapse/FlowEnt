namespace FriedSynapse.FlowEnt
{
    public class EchoAuthoring : AbstractAuthoring<Echo, EchoBuilder>, IGizmoDrawer
    {
#if UNITY_EDITOR
        void IGizmoDrawer.OnGizmosDrawing()
        {
            ((IGizmoDrawer)AnimationBuilder).OnGizmosDrawing();
        }
#endif
    }
}