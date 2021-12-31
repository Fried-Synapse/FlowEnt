using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class EchoOptionsTests : AbstractAnimationOptionsTests<Echo, EchoOptions>
    {
        protected override Echo CreateAnimation(float testTime)
            => new Echo(testTime);

        protected override Echo CreateAnimation(float testTime, EchoOptions options)
            => new Echo().SetOptions(options).SetTimeout(testTime);

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            EchoOptions echoOptions = default;

            yield return CreateTester()
                .Act(() => echoOptions = Variables.EchoOptionsBuilder.Build())
                .Assert(() =>
                {
                    Assert.AreEqual(Variables.EchoOptionsBuilder.Name, echoOptions.Name);
                    Assert.AreEqual(Variables.EchoOptionsBuilder.UpdateType, echoOptions.UpdateType);
                    Assert.AreEqual(Variables.EchoOptionsBuilder.AutoStart, echoOptions.AutoStart);
                    Assert.AreEqual(Variables.EchoOptionsBuilder.SkipFrames, echoOptions.SkipFrames);
                    Assert.AreEqual(Variables.EchoOptionsBuilder.Delay, echoOptions.Delay);
                    Assert.AreEqual(Variables.EchoOptionsBuilder.TimeScale, echoOptions.TimeScale);
                    if (Variables.EchoOptionsBuilder.HasTimeout)
                    {
                        Assert.AreEqual(Variables.EchoOptionsBuilder.Timeout, echoOptions.Timeout);
                    }
                    else
                    {
                        Assert.AreEqual(null, echoOptions.Timeout);
                    }
                    if (Variables.EchoOptionsBuilder.IsLoopCountInfinite)
                    {
                        Assert.AreEqual(null, echoOptions.LoopCount);
                    }
                    else
                    {
                        Assert.AreEqual(Variables.EchoOptionsBuilder.LoopCount, echoOptions.LoopCount);
                    }
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

        [UnityTest]
        public IEnumerator Timeout_ZeroValue()
        {
            const float timeout = 0f;

            yield return CreateTester()
                .Act(() => new Echo()
                            .SetTimeout(timeout)
                            .Start())
                .AssertTime(timeout)
                .Run();
        }

        [Test]
        public void Time_NegativeValue()
        {
            const float timeout = -2f;

            Assert.Throws<ArgumentException>(() => new Echo(timeout));
            Assert.Throws<ArgumentException>(() => new Echo().SetTimeout(timeout));
            Assert.Throws<ArgumentException>(() => new Echo(new EchoOptions() { Timeout = timeout }));
            Assert.Throws<ArgumentException>(() => new Echo(new EchoOptions().SetTimeout(timeout)));
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
                        .SetStopCondition((_) => flag)
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