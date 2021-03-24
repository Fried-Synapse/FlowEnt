
namespace FlowEnt
{
    public class EaseInBounce : EaseOutBounce
    {
        public override float GetValue(float t)
            => 1 - base.GetValue(1 - t);
    }
}