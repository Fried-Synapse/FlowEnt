
namespace FriedSynapse.FlowEnt.Easings
{
    public class EaseInQuad : IEasing
    {
        public float GetValue(float t)
            => t * t;
    }
}