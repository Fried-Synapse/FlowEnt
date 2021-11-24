
namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseInQuart : IEasing
    {
        public float GetValue(float t)
            => t * t * t * t;
    }
}