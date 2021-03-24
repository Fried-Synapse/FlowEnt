
namespace FlowEnt
{
    public class EaseOutBounce : IEasing
    {
        private const float N1 = 7.5625f;
        private const float D1 = 2.75f;

        public virtual float GetValue(float t)
        {
            if (t < 1 / D1)
            {
                return N1 * t * t;
            }
            else if (t < 2 / D1)
            {
                return N1 * (t -= 1.5f / D1) * t + 0.75f;
            }
            else if (t < 2.5 / D1)
            {
                return N1 * (t -= 2.25f / D1) * t + 0.9375f;
            }
            else
            {
                return N1 * (t -= 2.625f / D1) * t + 0.984375f;
            }
        }
    }
}