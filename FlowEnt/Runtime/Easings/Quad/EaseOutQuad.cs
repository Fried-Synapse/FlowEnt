
namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseOutQuad : IEasing
    {
        public float GetValue(float t)
            => 1 - ((1 - t) * (1 - t));
    }
}