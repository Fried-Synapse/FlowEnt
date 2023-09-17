using System;
using FluentAssertions;
using FluentAssertions.Numeric;
using UnityEngine;
using NUnit.Framework;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public static class FlowEntAssert
    {
        private const float Epsilon = 0.0001f;
        private const float TimeEpsilon = 0.03f;

        public static AndConstraint<NumericAssertions<float>> BeApproximatelyFloat(
            this NumericAssertions<float> parent,
            float expectedValue, string because = "", params object[] becauseArgs)
        {
            return parent.BeApproximately(expectedValue, Epsilon, because, becauseArgs);
        }
        
        public static AndConstraint<NumericAssertions<float>> BeApproximatelyTime(
            this NumericAssertions<float> parent,
            float expectedValue, string because = "", params object[] becauseArgs)
        {
            return parent.BeApproximately(expectedValue, TimeEpsilon, because, becauseArgs);
        }
        
        public static AndConstraint<NumericAssertions<double>> BeApproximatelyTime(
            this NumericAssertions<double> parent,
            float expectedValue, string because = "", params object[] becauseArgs)
        {
            return parent.BeApproximately(expectedValue, TimeEpsilon, because, becauseArgs);
        }

        [Obsolete("Use FluentAssertions. They are also wrong")]
        public static void AreEqual(float expected, float actual, float precision = Epsilon)
            => Assert.LessOrEqual(expected - actual, precision);

        [Obsolete("Use FluentAssertions. They are also wrong")]
        public static void AreEqual(Vector3 expected, Vector3 actual, float precision = Epsilon)
            => Assert.LessOrEqual((expected - actual).magnitude, precision);

        [Obsolete("Use FluentAssertions. They are also wrong")]
        public static void AreEqual(Quaternion expected, Quaternion actual, float precision = Epsilon)
            => Assert.LessOrEqual(Quaternion.Angle(expected, actual), precision);
    }
}