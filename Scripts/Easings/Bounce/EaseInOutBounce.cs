
namespace FriedSynapse.FlowEnt
{
    public class EaseInOutBounce : EaseOutBounce
    {
        public override float GetValue(float t)
            => t < 0.5f
                  ? (1 - base.GetValue(1 - (2 * t))) / 2
                  : (1 + base.GetValue((2 * t) - 1)) / 2;
    }
}