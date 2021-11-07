namespace FriedSynapse.FlowEnt
{
    public class BounceEasing : AbstractOscilationEasing
    {
        public BounceEasing(int bounces = DefaultReps, bool reverse = DefaultReverse, float bounciness = DefaultIntensity) : base(bounces, reverse, bounciness)
        {
        }

        protected override string RepsName => "bounces";
        protected override string IntensityName => "bounciness";

        public override float GetValue(float t)
        {
            return GetSegmentAndValue(t).Item2;
        }
    }
}
