using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class TweenTests : AbstractEngineTests
    {
        [UnityTest]
        public IEnumerator Name()
        {
            const float time = 0.1f;
            const string name = "Billy";
            Tween tween = default;

            yield return CreateTester()
                .Arrange(() => tween = new Tween(time))
                .Act(() => tween.SetName(name).Start())
                .AssertTime(time)
                .Assert(() => Assert.AreEqual(name, tween.Name))
                .Run();
        }

        [UnityTest]
        public IEnumerator PlayState_Values()
        {
            bool isBuilding = false;
            bool isWaiting = false;
            bool isPlaying = false;
            bool isPaused = false;
            Tween tween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    tween = new Tween(TestTime)
                                   .SetDelay(HalfTestTime);

                    isBuilding = tween.PlayState == PlayState.Building;

                    tween.Start();

                    Flow flowControl = new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => isWaiting = tween.PlayState == PlayState.Waiting))
                                    .Queue(new Tween(QuarterTestTime))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => isPlaying = tween.PlayState == PlayState.Playing))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => tween.Pause()))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => isPaused = tween.PlayState == PlayState.Paused))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => tween.Resume()))
                                    .Start();
                    return tween;
                })
                .AssertTime(DoubleTestTime)
                .Assert(() =>
                {
                    Assert.True(isBuilding);
                    Assert.True(isWaiting);
                    Assert.True(isPlaying);
                    Assert.True(isPaused);
                    Assert.True(tween.PlayState == PlayState.Finished);
                })
                .Run($"Testing different states on {nameof(PlayState_Values)}", DoubleTestTime);
        }

        [UnityTest]
        public IEnumerator Start()
        {
            yield return CreateTester()
                .Act(() => new Tween(TestTime).Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator StartAsync()
        {
            Task task = null;

            IEnumerator customWaiter()
            {
                while (!task.IsCompleted)
                {
                    yield return null;
                }
            }

            yield return CreateTester()
                .Act(() =>
                {
                    Tween tween = new Tween(TestTime);
                    task = tween.StartAsync();
                    return tween;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Start_WaitAsync()
        {
            Task task = null;

            IEnumerator customWaiter()
            {
                while (!task.IsCompleted)
                {
                    yield return null;
                }
            }

            yield return CreateTester()
                .Act(() =>
                {
                    Tween tween = new Tween(TestTime).Start();
                    task = tween.AsAsync();
                    return tween;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Start_Started()
        {
            Exception startException = null;
            Tween tween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    tween = new Tween(TestTime).Start();
                    try
                    {
                        tween.Start();
                    }
                    catch (Exception ex)
                    {
                        startException = ex;
                    }

                    return tween;
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(startException != null && startException is AnimationException animationException && animationException.Animation is Tween);
                    Assert.Throws<AnimationException>(() => tween.Start());
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator PauseResume()
        {
            yield return CreateTester()
                .Act(() =>
                {
                    Tween tween = new Tween()
                                    .SetTime(HalfTestTime)
                                    .Start();
                    Flow flowControl = new Flow()
                                    .Queue(new Tween(HalfTestTime / 2f).OnCompleted(() => tween.Pause()))
                                    .Queue(new Tween(HalfTestTime / 2f).OnCompleted(() => tween.Resume()))
                                    .Start();
                    return tween;
                })
                .AssertTime(ThreeQuartersTestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop()
        {
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    Tween tween = new Tween()
                                    .SetTime(HalfTestTime)
                                    .OnCompleted(() => hasFinished = true)
                                    .Start();
                    return new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => tween.Stop()))
                                    .Queue(new Tween(HalfTestTime))
                                    .Start();
                })
                .Assert(() => Assert.AreEqual(false, hasFinished))
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop_TriggerOnCompleted()
        {
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    Tween tween = new Tween()
                        .SetTime(HalfTestTime)
                        .OnCompleted(() => hasFinished = true)
                        .Start();
                    return new Flow()
                        .Queue(new Tween(QuarterTestTime).OnCompleted(() => tween.Stop(true)))
                        .Queue(new Tween(HalfTestTime))
                        .Start();
                })
                .Assert(() => Assert.AreEqual(true, hasFinished))
                .Run();
        }

        [UnityTest]
        public IEnumerator Reset()
        {
            const int runs = 3;
            int runned = 0;
            Tween tween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    tween = new Tween(TestTime / runs)
                        .OnCompleted(() =>
                        {
                            runned++;
                            if (runned < runs)
                            {
                                tween.Reset().Start();
                            }
                        })
                        .Start();
                    return new Tween(TestTime).Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(PlayState.Finished, tween.PlayState);
                    Assert.AreEqual(runs, runned);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ResetUsingFlow()
        {
            Tween tween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    tween = new Tween()
                        .SetTime(TestTime / 2f);
                    return new Flow()
                        .Queue(tween)
                        .QueueDeferred(() => tween.Reset())
                        .Start();
                })
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(PlayState.Finished, tween.PlayState))
                .Run();
        }
    }
}
