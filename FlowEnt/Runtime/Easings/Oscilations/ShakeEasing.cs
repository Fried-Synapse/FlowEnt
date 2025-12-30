namespace FriedSynapse.FlowEnt.Easings
{
    public class ShakeEasing : AbstractOscilationEasing
    {
        public ShakeEasing(int shakes = DefaultReps, bool reverse = DefaultReverse, bool flip = false, float bounciness = DefaultIntensity) : base(shakes, reverse, bounciness)
        {
            this.flip = flip;
        }

        protected override string RepsName => "shakes";
        protected override string IntensityName => "bounciness";
        private readonly bool flip;

        public override float GetValue(float t)
        {
            (int segment, float value) = GetSegmentAndValue(t);
            return (segment % 2 == (flip ? 1 : 0)) ? value : -value;
        }
    }
}
