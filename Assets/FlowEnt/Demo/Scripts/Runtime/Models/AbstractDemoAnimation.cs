namespace FriedSynapse.FlowEnt.Demo
{
    public interface IEditorDrawer
    {
#if UNITY_EDITOR
        public void OnDraw();
#endif
    }

    public abstract class AbstractDemoAnimation
    {
        public abstract AbstractAnimation GetAnimation();
    }
}
