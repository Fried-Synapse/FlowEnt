namespace FriedSynapse.FlowEnt
{
    public class TweenAuthoring : AbstractAuthoring<Tween, TweenBuilder>, IGizmoDrawer
    {
#if UNITY_EDITOR
        void IGizmoDrawer.OnGizmosDrawing()
        {
            ((IGizmoDrawer)AnimationBuilder).OnGizmosDrawing();
        }
#endif
    }
}