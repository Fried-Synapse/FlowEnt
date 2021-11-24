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
        public IEnumerator PlayState_Values()
        {
            const float time = 1f;
            const float delay = 1f;
            bool isBuilding = false;
            bool isWaiting = false;
            bool isPlaying = false;
            bool isPaused = false;
            Tween tween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    tween = new Tween(time)
                                   .SetDelay(delay);

                    isBuilding = tween.PlayState == PlayState.Building;

                    tween.Start();

                    Flow flowControl = new Flow()
                                    .Queue(new Tween(delay / 2f).OnCompleted(() => isWaiting = tween.PlayState == PlayState.Waiting))
                                    .Queue(new Tween(delay / 2f))
                                    .Queue(new Tween(time / 4f).OnCompleted(() => isPlaying = tween.PlayState == PlayState.Playing))
                                    .Queue(new Tween(time / 4f).OnCompleted(() => tween.Pause()))
                                    .Queue(new Tween(time / 4f).OnCompleted(() => isPaused = tween.PlayState == PlayState.Paused))
                                    .Queue(new Tween(time / 4f).OnCompleted(() => tween.Resume()))
                                    .Start();
                    return tween;
                })
                .AssertTime(delay + (time * 1.5f))
                .Assert(() =>
                {
                    Assert.True(isBuilding);
                    Assert.True(isWaiting);
                    Assert.True(isPlaying);
                    Assert.True(isPaused);
                    Assert.True(tween.PlayState == PlayState.Finished);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator Start()
        {
            const float time = 1f;

            yield return CreateTester()
                .Act(() => new Tween(time).Start())
                .AssertTime(time)
                .Run();
        }

        [UnityTest]
        public IEnumerator StartAsync()
        {
            const float time = 1f;
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
                    Tween tween = new Tween(time);
                    task = tween.StartAsync();
                    return tween;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Start_WaitAsync()
        {
            const float time = 1f;
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
                    Tween tween = new Tween(time).Start();
                    task = tween.AsAsync();
                    return tween;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(time)
                .Run();
        }

        [UnityTest]
        public IEnumerator Start_Started()
        {
            const float time = 1f;
            Exception startException = null;
            Tween tween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    tween = new Tween(time).Start();
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
                .AssertTime(time)
                .Assert(() =>
                {
                    Assert.True(startException != null && startException is TweenException);
                    Assert.Throws<TweenException>(() => tween.Start());
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator PauseResume()
        {
            const float testTime = 2f;

            yield return CreateTester()
                .Act(() =>
                {
                    Tween tween = new Tween()
                                    .SetTime(testTime)
                                    .Start();
                    Flow flowControl = new Flow()
                                    .Queue(new Tween(testTime / 2f).OnCompleted(() => tween.Pause()))
                                    .Queue(new Tween(testTime / 2f).OnCompleted(() => tween.Resume()))
                                    .Start();
                    return tween;
                })
                .AssertTime(testTime * 1.5f)
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop()
        {
            const float testTime = 1f;
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    Tween tween = new Tween()
                                    .SetTime(testTime)
                                    .OnCompleted(() => hasFinished = true)
                                    .Start();
                    return new Flow()
                                    .Queue(new Tween(testTime / 2f).OnCompleted(() => tween.Stop()))
                                    .Queue(new Tween(testTime))
                                    .Start();
                })
                .Assert(() => Assert.AreEqual(false, hasFinished))
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop_TriggerOnCompleted()
        {
            const float testTime = 1f;
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    Tween tween = new Tween()
                        .SetTime(testTime)
                        .OnCompleted(() => hasFinished = true)
                        .Start();
                    return new Flow()
                        .Queue(new Tween(testTime / 2f).OnCompleted(() => tween.Stop(true)))
                        .Queue(new Tween(testTime))
                        .Start();
                })
                .Assert(() => Assert.AreEqual(true, hasFinished))
                .Run();
        }

        [UnityTest]
        public IEnumerator Reset()
        {
            const float testTime = 1f;
            const int runs = 3;
            int runned = 0;
            Tween tween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    tween = new Tween(testTime / runs)
                        .OnCompleted(() =>
                        {
                            runned++;
                            if (runned < runs)
                            {
                                tween.Reset().Start();
                            }
                        })
                        .Start();
                    return new Tween(testTime).Start();
                })
                .AssertTime(testTime)
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
            const float testTime = 1f;
            Tween tween = null;

            yield return CreateTester()
                .Act(() =>
                {
                    tween = new Tween()
                        .SetTime(testTime / 2f);
                    return new Flow()
                        .Queue(tween)
                        .QueueDeferred(() => tween.Reset())
                        .Start();
                })
                .AssertTime(testTime)
                .Assert(() => Assert.AreEqual(PlayState.Finished, tween.PlayState))
                .Run();
        }
    }
}
