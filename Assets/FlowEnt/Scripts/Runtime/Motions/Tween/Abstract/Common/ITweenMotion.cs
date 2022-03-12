using FriedSynapse.FlowEnt.Motions.Abstract;

namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public interface ITweenMotion : IMotion
    {
        public void OnStart();
        public void OnUpdate(float t);
        public void OnLoopStart();
        public void OnLoopComplete();
        public void OnComplete();
    }
}
