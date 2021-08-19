
namespace FriedSynapse.FlowEnt
{
    public class EaseInQuad : IEasing
    {
        public float GetValue(float t)
            => t * t;
    }
}