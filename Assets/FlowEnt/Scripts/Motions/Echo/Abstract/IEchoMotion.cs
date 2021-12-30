namespace FriedSynapse.FlowEnt.Motions.Echo.Abstract
{
    public interface IEchoMotion
    {
        public void OnStart();
        public void OnUpdate(float deltaTime);
        public void OnLoopComplete();
        public void OnComplete();
    }
}
