using FriedSynapse.FlowEnt.Motions.Abstract;

namespace FriedSynapse.FlowEnt.Motions.Echo.Abstract
{
    public interface IEchoMotion : IMotion
    {
        public void OnStart();
        public void OnUpdate(float deltaTime);
        public void OnLoopStart();
        public void OnLoopComplete();
        public void OnComplete();
    }
}
