
namespace FlowEnt
{
    public class EaseInCubic : IEasing
    {
        public float GetValue(float t)
            => t * t * t;
    }
}