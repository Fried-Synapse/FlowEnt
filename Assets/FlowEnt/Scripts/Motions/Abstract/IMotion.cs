namespace FriedSynapse.FlowEnt.Motions.Abstract
{
    public interface IMotion
    {
        public void OnStart();
        public void OnUpdate(float t);
        public void OnLoopComplete();
        public void OnComplete();
    }
}
