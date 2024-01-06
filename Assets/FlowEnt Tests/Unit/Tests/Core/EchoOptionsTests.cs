using System;
using System.Collections;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public static class EchoTestsValues
    {
        public static readonly float[] timeoutValues = { 0f, -1f };
    }

    public class EchoOptionsTests : AbstractAnimationOptionsTests<Echo, EchoOptions>
    {
        protected override Echo CreateAnimation(float testTime)
            => new(testTime);

        protected override Echo CreateAnimation(float testTime, EchoOptions options)
            => new Echo().SetOptions(options).SetTimeout(testTime);

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            EchoOptions echoOptions = default;

            yield return CreateTester()
                .Act(() => echoOptions = Variables.Echo.Options.Build())
                .Assert(() =>
                {
                    echoOptions.Name.Should().Be(Variables.Echo.Options.Name);
                    echoOptions.UpdateType.Should().Be(Variables.Echo.Options.UpdateType);
                    echoOptions.AutoStart.Should().Be(Variables.Echo.Options.AutoStart);
                    echoOptions.SkipFrames.Should().Be(Variables.Echo.Options.SkipFrames);
                    echoOptions.Delay.Should().Be(Variables.Echo.Options.Delay);
                    echoOptions.TimeScale.Should().Be(Variables.Echo.Options.TimeScale);
                    echoOptions.Timeout.Should().Be(Variables.Echo.Options.Timeout);
                    echoOptions.LoopCount.Should().Be(Variables.Echo.Options.LoopCount);
                })
                .Run();
        }

        #endregion

        #region Timeout

        [UnityTest]
        public IEnumerator Timeout_Default()
        {
            yield return CreateTester()
                .Act(() => new Echo()
                    .SetTimeout(TestTime)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Timeout_Constructor()
        {
            yield return CreateTester()
                .Act(() => new Echo(TestTime)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Timeout_WithOptions()
        {
            yield return CreateTester()
                .Act(() => new Echo()
                    .SetOptions(new EchoOptions().SetTimeout(TestTime))
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [Test]
        public void Timeout_Invalid([ValueSource(typeof(EchoTestsValues), nameof(EchoTestsValues.timeoutValues))] float timeout)
        {
            Func<Echo> act = () => new Echo(timeout);
            act.Should().Throw<ArgumentException>();
            act = () => new Echo().SetTimeout(timeout);
            act.Should().Throw<ArgumentException>();
            act = () => new Echo(new EchoOptions { Timeout = timeout });
            act.Should().Throw<ArgumentException>();
            act = () => new Echo(new EchoOptions().SetTimeout(timeout));
            act.Should().Throw<ArgumentException>();
        }

        #endregion

        #region SetStopCondition

        [UnityTest]
        public IEnumerator SetStopCondition_Timeout()
        {
            yield return CreateTester()
                .Act(() => new Echo()
                    .SetStopCondition((time) => time > TestTime)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator SetStopCondition_TweenControl()
        {
            yield return CreateTester()
                .Act(() =>
                {
                    bool flag = false;
                    Tween tween = new Tween(TestTime).OnCompleted(() => flag = true).Start();

                    return new Echo()
                        .SetStopCondition(_ => flag)
                        .Start();
                })
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator SetStopCondition_MultiCondition()
        {
            yield return CreateTester()
                .Act(() => new Echo()
                    .SetStopCondition((time) => time > TestTime * 2)
                    .SetStopCondition((time) => time > TestTime)
                    .Start())
                .AssertTime(TestTime)
                .Run();
        }

        #endregion
    }
}