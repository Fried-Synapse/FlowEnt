using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            Assert.Throws<ArgumentException>(() => FlowEntController.Instance.TimeScale = testTimeScale);
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
                                .SetTime(HalfTestTime * 2)
                                .Start();
                    Task.Delay((int)(HalfTestTime * 1000)).ContinueWith(_ => FlowEntController.Instance.Pause());
                    Task.Delay((int)(HalfTestTime * 2000)).ContinueWith(_ => FlowEntController.Instance.Resume());
                    return tween1;
                })
                .AssertTime(ThreeQuartersTestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(tween1.PlayState, PlayState.Finished);
                    Assert.AreEqual(tween2.PlayState, PlayState.Playing);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop()
        {
            const int tweensCount = 10;
            const int expectedCompletedTweens = 0;
            int comletedTweens = 0;

            yield return CreateTester()
                .Act(() =>
                {
                    List<Tween> tweens = new List<Tween>();
                    for (int i = 0; i < tweensCount; i++)
                    {
                        tweens.Add(new Tween(TestTime).OnCompleted(() => comletedTweens++).Start());
                    }
                    return new Tween(HalfTestTime).OnCompleted(() => FlowEntController.Instance.Stop()).Start();
                })
                .AssertTime(HalfTestTime)
                .Assert(() => Assert.AreEqual(expectedCompletedTweens, comletedTweens))
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
                    List<Tween> tweens = new List<Tween>();
                    for (int i = 0; i < tweensCount; i++)
                    {
                        tweens.Add(new Tween(TestTime * 2).OnCompleted(() => comletedTweens++).Start());
                    }
                    return new Tween(TestTime).OnCompleted(() => FlowEntController.Instance.Stop(true)).Start();
                })
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(expectedCompletedTweens, comletedTweens))
                .Run();
        }

        //TODO find a way to run this test all the time
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
        [UnityTest]
        public IEnumerator Update_TriggerException()
        {
            bool hasThrownException = false;

            yield return CreateTester()
                .Act(() => new Tween(TestTime).OnUpdating((t) =>
                {
                    if (t > 0.1f && !hasThrownException)
                    {
                        hasThrownException = true;
                        throw new Exception();
                    }
                }).Start())
                .AssertTime(TestTime)
                .Assert(() => LogAssert.Expect(LogType.Error, new Regex("Origin of animation that generated the exception")))
                .Run();
        }
#endif
    }
}