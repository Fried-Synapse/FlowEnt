
namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseInQuint : IEasing
    {
        public float GetValue(float t)
            => t * t * t * t * t;
    }
}