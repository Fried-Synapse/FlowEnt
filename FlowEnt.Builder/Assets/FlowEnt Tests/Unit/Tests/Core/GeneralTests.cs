using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine.TestTools;
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
using System.Text.RegularExpressions;
using UnityEngine;
#endif

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class GeneralTests : AbstractEngineTests
    {
        [UnityTest]
        public IEnumerator TimeScale()
        {
            const float timeScale = 2f;

            yield return CreateTester()
                .Act(() =>
                {
                    FlowEntController.Instance.TimeScale = timeScale;
                    return new Flow()
                        .Queue(new Tween().SetTime(TestTime))
                        .Start();
                })
                .AssertTime(TestTime / timeScale)
                .Abrogate(() => FlowEntController.Instance.TimeScale = 1f)
                .Run();
        }

        [Test]
        public void TimeScale_NegativeTimeScale()
        {
            const float testTimeScale = -1f;
            Action act = () => FlowEntController.Instance.TimeScale = testTimeScale;
            act.Should().Throw<ArgumentException>();
        }

        [UnityTest]
        public IEnumerator PauseResume()
        {
            Tween tween1 = null;
            Tween tween2 = null;

            yield return CreateTester()
                .Act(() =>
                {
                    tween1 = new Tween()
                        .SetTime(HalfTestTime)
                        .Start();
                    tween2 = new Tween()
                        .SetTime(TestTime)
                        .Start();
                    Task.Delay((int)(QuarterTestTime * 1000)).ContinueWith(_ => FlowEntController.Instance.Pause());
                    Task.Delay((int)(HalfTestTime * 1000)).ContinueWith(_ => FlowEntController.Instance.Resume());
                    return tween1;
                })
                .AssertTime(ThreeQuartersTestTime)
                .Assert(() =>
                {
                    tween1.PlayState.Should().Be(PlayState.Finished);
                    tween2.PlayState.Should().Be(PlayState.Playing);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop()
        {
            const int tweensCount = 10;
            const int expectedCompletedTweens = 0;
            int completedTweens = 0;

            yield return CreateTester()
                .Act(() =>
                {
                    for (int i = 0; i < tweensCount; i++)
                    {
                        new Tween(TestTime).OnCompleted(() => completedTweens++).Start();
                    }
                    return new Tween(HalfTestTime).OnCompleted(() => FlowEntController.Instance.Stop()).Start();
                })
                .AssertTime(HalfTestTime)
                .Assert(() => completedTweens.Should().Be(expectedCompletedTweens))
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop_TriggerOnCompletes()
        {
            const int tweensCount = 10;
            const int expectedCompletedTweens = tweensCount;
            int comletedTweens = 0;

            yield return CreateTester()
                .Act(() =>
                {
                    List<Tween> tweens = new();
                    for (int i = 0; i < tweensCount; i++)
                    {
                        tweens.Add(new Tween(TestTime * 2).OnCompleted(() => comletedTweens++).Start());
                    }
                    return new Tween(TestTime).OnCompleted(() => FlowEntController.Instance.Stop(true)).Start();
                })
                .AssertTime(TestTime)
                .Assert(() => comletedTweens.Should().Be(expectedCompletedTweens))
                .Run();
        }

#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
        [UnityTest]
        public IEnumerator Update_TriggerException()
        {
            bool hasThrownException = false;

            yield return CreateTester()
                .Act(() => new Tween(TestTime).OnUpdating(t =>
                {
                    if (t > 0.1f && !hasThrownException)
                    {
                        hasThrownException = true;
                        throw new Exception();
                    }
                }).Start())
                .AssertTime(TestTime)
                .Assert(() => LogAssert.Expect(LogType.Error, new Regex(@"(?=.*\[FlowEnt\])(?=.*Update Exception)(?=.*on Animation)")))
                .Run();
        }
#endif
    }
}