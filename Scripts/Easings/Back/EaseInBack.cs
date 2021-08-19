
namespace FriedSynapse.FlowEnt
{
    public class EaseInBack : IEasing
    {
        private const float C1 = 1.70158f;
        private const float C3 = C1 + 1;

        public float GetValue(float t)
            => C3 * t * t * t - C1 * t * t;
    }
}