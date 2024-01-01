namespace FriedSynapse.FlowEnt
{
    public class TweenAuthoring : AbstractAuthoring<Tween, TweenBuilder>, IGizmoDrawer
    {
#if UNITY_EDITOR
        void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
        {
            ((IGizmoDrawer)AnimationBuilder).OnGizmosDrawing(options);
        }
#endif
    }
}