namespace FriedSynapse.FlowEnt
{
    public class TweenAuthoring : AbstractAuthoring<Tween, TweenBuilder>, IGizmoDrawer
    {
        void IGizmoDrawer.OnGizmosDrawing()
        {
#if UNITY_EDITOR
            ((IGizmoDrawer)AnimationBuilder).OnGizmosDrawing();
#endif
        }
    }
}