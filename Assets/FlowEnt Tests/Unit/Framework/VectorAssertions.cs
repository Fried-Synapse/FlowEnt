using System;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public static class UnityExtensions
    {
        public static Vector3Assertions Should(this Vector3 instance)
        {
            return new Vector3Assertions(instance);
        }

        public static QuaternionAssertions Should(this Quaternion instance)
        {
            return new QuaternionAssertions(instance);
        }
    }

    public class Vector3Assertions : ReferenceTypeAssertions<Vector3, Vector3Assertions>
    {
        public Vector3Assertions(Vector3 subject) : base(subject)
        {
        }

        public AndConstraint<Vector3Assertions> Be(
            Vector3 expectedValue, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject == expectedValue)
                .FailWith($"Expected {expectedValue}, but found {Subject}.");

            return new AndConstraint<Vector3Assertions>(this);
        }

        public AndConstraint<Vector3Assertions> BeApproximately(
            Vector3 expectedValue, float epsilon = NumericAssertionsExtensions.Epsilon, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Math.Abs(Subject.magnitude - expectedValue.magnitude) < epsilon)
                .FailWith($"Expected {expectedValue}, but found {Subject}.");

            return new AndConstraint<Vector3Assertions>(this);
        }

        protected override string Identifier => nameof(Vector3);
    }

    public class QuaternionAssertions : ReferenceTypeAssertions<Quaternion, QuaternionAssertions>
    {
        public QuaternionAssertions(Quaternion subject) : base(subject)
        {
        }

        public AndConstraint<QuaternionAssertions> Be(
            Quaternion expectedValue, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject == expectedValue)
                .FailWith($"Expected {expectedValue}, but found {Subject}.");

            return new AndConstraint<QuaternionAssertions>(this);
        }

        public AndConstraint<QuaternionAssertions> BeApproximately(
            Quaternion expectedValue, float epsilon = NumericAssertionsExtensions.Epsilon, string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Math.Abs(Quaternion.Angle(Subject, expectedValue)) < epsilon)
                .FailWith($"Expected {expectedValue}, but found {Subject}.");

            return new AndConstraint<QuaternionAssertions>(this);
        }

        protected override string Identifier => nameof(Quaternion);
    }
}