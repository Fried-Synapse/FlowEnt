namespace FriedSynapse.FlowEnt.Easings
{
    /// <summary>
    /// Reverses the specified easing using a unitary inversion.
    /// </summary>
    public class ReverseEasing : IEasing
    {
        public ReverseEasing(IEasing easing)
        {
            this.easing = easing;
        }

        private readonly IEasing easing;

        public float GetValue(float t)
            => 1 - easing.GetValue(t);
    }
}
