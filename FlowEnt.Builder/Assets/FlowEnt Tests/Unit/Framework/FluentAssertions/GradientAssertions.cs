using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class GradientAssertions : ReferenceTypeAssertions<Gradient, GradientAssertions>
    {
        public GradientAssertions(Gradient subject) : base(subject)
        {
        }

        protected override string Identifier => nameof(Gradient);

        public AndConstraint<GradientAssertions> Be(
            Gradient expected, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(GradientOperations.AreEqual(Subject, expected))
                .WithDefaultIdentifier(Identifier)
                .FailWith("Expected {context} to be {0}{reason}, but found {1}.", expected,
                    Subject);

            return new AndConstraint<GradientAssertions>(this);
        }
    }

    public static class GradientExtensions
    {
        public static GradientAssertions Should(this Gradient instance)
            => new(instance);
    }
}