using System;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class EchoTests : AbstractEngineTests
    {
        [UnityTest]
        public IEnumerator Name()
        {
            const float time = 0.1f;
            const string name = "Billy";
            Echo echo = default;

            yield return CreateTester()
                .Arrange(() => echo = new Echo(time))
                .Act(() => echo.SetName(name).Start())
                .AssertTime(time)
                .Assert(() => Assert.AreEqual(name, echo.Name))
                .Run();
        }

        [UnityTest]
        public IEnumerator PlayState_Values()
        {
            const float delay = 1f;
            bool isBuilding = false;
            bool isWaiting = false;
            bool isPlaying = false;
            bool isPaused = false;
            Echo echo = null;

            yield return CreateTester()
                .Act(() =>
                {
                    echo = new Echo(TestTime)
                                   .SetDelay(delay);

                    isBuilding = echo.PlayState == PlayState.Building;

                    echo.Start();

                    Flow flowControl = new Flow()
                                    .Queue(new Echo(delay / 2f).OnCompleted(() => isWaiting = echo.PlayState == PlayState.Waiting))
                                    .Queue(new Echo(delay / 2f))
                                    .Queue(new Echo(TestTime / 4f).OnCompleted(() => isPlaying = echo.PlayState == PlayState.Playing))
                                    .Queue(new Echo(TestTime / 4f).OnCompleted(() => echo.Pause()))
                                    .Queue(new Echo(TestTime / 4f).OnCompleted(() => isPaused = echo.PlayState == PlayState.Paused))
                                    .Queue(new Echo(TestTime / 4f).OnCompleted(() => echo.Resume()))
                                    .Start();
                    return echo;
                })
                .AssertTime(delay + (TestTime * 1.5f))
                .Assert(() =>
                {
                    Assert.True(isBuilding);
                    Assert.True(isWaiting);
                    Assert.True(isPlaying);
                    Assert.True(isPaused);
                    Assert.True(echo.PlayState == PlayState.Finished);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator Start()
        {
            yield return CreateTester()
                .Act(() => new Echo(TestTime).Start())
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
                    Echo echo = new Echo(TestTime);
                    task = echo.StartAsync();
                    return echo;
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
                    Echo echo = new Echo(TestTime).Start();
                    task = echo.AsAsync();
                    return echo;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Start_Started()
        {
            Exception startException = null;
            Echo echo = null;

            yield return CreateTester()
                .Act(() =>
                {
                    echo = new Echo(TestTime).Start();
                    try
                    {
                        echo.Start();
                    }
                    catch (Exception ex)
                    {
                        startException = ex;
                    }

                    return echo;
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(startException != null && startException is AnimationException animationException && animationException.Animation is Echo);
                    Assert.Throws<AnimationException>(() => echo.Start());
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator PauseResume()
        {
            const float testTime = 1f;

            yield return CreateTester()
                .Act(() =>
                {
                    Echo echo = new Echo()
                                    .SetTimeout(testTime)
                                    .Start();
                    Flow flowControl = new Flow()
                                    .Queue(new Echo(testTime / 2f).OnCompleted(() => echo.Pause()))
                                    .Queue(new Echo(testTime / 2f).OnCompleted(() => echo.Resume()))
                                    .Start();
                    return echo;
                })
                .AssertTime(testTime * 1.5f)
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop()
        {
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    Echo echo = new Echo()
                                    .SetTimeout(TestTime)
                                    .OnCompleted(() => hasFinished = true)
                                    .Start();
                    return new Flow()
                                    .Queue(new Echo(TestTime / 2f).OnCompleted(() => echo.Stop()))
                                    .Queue(new Echo(TestTime))
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
                    Echo echo = new Echo()
                        .SetTimeout(TestTime)
                        .OnCompleted(() => hasFinished = true)
                        .Start();
                    return new Flow()
                        .Queue(new Echo(TestTime / 2f).OnCompleted(() => echo.Stop(true)))
                        .Queue(new Echo(TestTime))
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
            Echo echo = null;

            yield return CreateTester()
                .Act(() =>
                {
                    echo = new Echo(TestTime / runs)
                        .OnCompleted(() =>
                        {
                            runned++;
                            if (runned < runs)
                            {
                                echo.Reset().Start();
                            }
                        })
                        .Start();
                    return new Echo(TestTime).Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(PlayState.Finished, echo.PlayState);
                    Assert.AreEqual(runs, runned);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ResetUsingFlow()
        {
            Echo echo = null;

            yield return CreateTester()
                .Act(() =>
                {
                    echo = new Echo()
                        .SetTimeout(TestTime / 2f);
                    return new Flow()
                        .Queue(echo)
                        .QueueDeferred(() => echo.Reset())
                        .Start();
                })
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(PlayState.Finished, echo.PlayState))
                .Run();
        }
    }
}
