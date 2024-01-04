namespace FriedSynapse.FlowEnt
{
    public interface IGizmoDrawer
    {
#if UNITY_EDITOR
        public void OnGizmosDrawing(GizmoOptions options);
#endif
    }
}