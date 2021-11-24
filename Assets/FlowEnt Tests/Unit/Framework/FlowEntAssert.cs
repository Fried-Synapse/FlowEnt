using UnityEngine;
using NUnit.Framework;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public static class FlowEntAssert
    {
        private const float Epsilon = 0.0001f;

        public static void Equal(float expected, float actual, float precision = Epsilon)
            => Assert.LessOrEqual(expected - actual, precision);

        public static void Equal(Vector3 expected, Vector3 actual, float precision = Epsilon)
            => Assert.LessOrEqual((expected - actual).magnitude, precision);

        public static void Equal(Quaternion expected, Quaternion actual, float precision = Epsilon)
            => Assert.LessOrEqual(Quaternion.Angle(expected, actual), precision);


        public static void Time(float expected, float actual, float errorMargin = 0.03f)
        {
            Assert.GreaterOrEqual(actual, expected - errorMargin);
            Assert.Less(actual, expected + errorMargin);
        }
    }
}
