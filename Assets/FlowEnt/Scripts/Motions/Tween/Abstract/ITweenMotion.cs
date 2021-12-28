namespace FriedSynapse.FlowEnt.Motions.Tween.Abstract
{
    public interface ITweenMotion
    {
        public void OnStart();
        public void OnUpdate(float t);
        public void OnLoopComplete();
        public void OnComplete();
    }
}
