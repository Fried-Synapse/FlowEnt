namespace FriedSynapse.FlowEnt.Demo
{
    public interface IDraw
    {
        public void OnDraw();
    }

    public abstract class AbstractDemoAnimation
    {
        public abstract AbstractAnimation GetAnimation();
    }
}
