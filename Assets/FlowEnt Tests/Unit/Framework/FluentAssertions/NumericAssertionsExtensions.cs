using FluentAssertions;
using FluentAssertions.Numeric;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public static class NumericAssertionsExtensions
    {
        public const float Epsilon = 0.0001f;
        private const float TimeEpsilon = 0.01f;

        public static AndConstraint<NumericAssertions<float>> BeApproximatelyFloat(
            this NumericAssertions<float> assertions,
            float expectedValue, string because = "", params object[] becauseArgs)
        {
            return assertions.BeApproximately(expectedValue, Epsilon, because, becauseArgs);
        }

        public static AndConstraint<NumericAssertions<float>> BeApproximatelyAngle(
            this NumericAssertions<float> assertions,
            float expectedValue, string because = "", params object[] becauseArgs)
        {
            const int fullCircle = 360;
            while (expectedValue < 0)
            {
                expectedValue += fullCircle;
            }
            
            while (expectedValue > fullCircle)
            {
                expectedValue -= fullCircle;
            }

            return assertions.BeApproximately(expectedValue, TimeEpsilon, because, becauseArgs);
        }

        public static AndConstraint<NumericAssertions<float>> BeApproximatelyTime(
            this NumericAssertions<float> assertions,
            float expectedValue, string because = "", params object[] becauseArgs)
        {
            return assertions.BeApproximately(expectedValue, TimeEpsilon, because, becauseArgs);
        }

        public static AndConstraint<NumericAssertions<double>> BeApproximatelyTime(
            this NumericAssertions<double> assertions,
            double expectedValue, string because = "", params object[] becauseArgs)
        {
            return assertions.BeApproximately(expectedValue, TimeEpsilon, because, becauseArgs);
        }
    }
}