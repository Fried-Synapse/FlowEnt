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
            const float time = 1f;
            const float delay = 1f;
            bool isBuilding = false;
            bool isWaiting = false;
            bool isPlaying = false;
            bool isPaused = false;
            Echo echo = null;

            yield return CreateTester()
                .Act(() =>
                {
                    echo = new Echo(time)
                                   .SetDelay(delay);

                    isBuilding = echo.PlayState == PlayState.Building;

                    echo.Start();

                    Flow flowControl = new Flow()
                                    .Queue(new Echo(delay / 2f).OnCompleted(() => isWaiting = echo.PlayState == PlayState.Waiting))
                                    .Queue(new Echo(delay / 2f))
                                    .Queue(new Echo(time / 4f).OnCompleted(() => isPlaying = echo.PlayState == PlayState.Playing))
                                    .Queue(new Echo(time / 4f).OnCompleted(() => echo.Pause()))
                                    .Queue(new Echo(time / 4f).OnCompleted(() => isPaused = echo.PlayState == PlayState.Paused))
                                    .Queue(new Echo(time / 4f).OnCompleted(() => echo.Resume()))
                                    .Start();
                    return echo;
                })
                .AssertTime(delay + (time * 1.5f))
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
            const float time = 1f;

            yield return CreateTester()
                .Act(() => new Echo(time).Start())
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
                    Echo echo = new Echo(time);
                    task = echo.StartAsync();
                    return echo;
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
                    Echo echo = new Echo(time).Start();
                    task = echo.AsAsync();
                    return echo;
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
            Echo echo = null;

            yield return CreateTester()
                .Act(() =>
                {
                    echo = new Echo(time).Start();
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
                .AssertTime(time)
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
            const float testTime = 2f;

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
            const float testTime = 1f;
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    Echo echo = new Echo()
                                    .SetTimeout(testTime)
                                    .OnCompleted(() => hasFinished = true)
                                    .Start();
                    return new Flow()
                                    .Queue(new Echo(testTime / 2f).OnCompleted(() => echo.Stop()))
                                    .Queue(new Echo(testTime))
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
                    Echo echo = new Echo()
                        .SetTimeout(testTime)
                        .OnCompleted(() => hasFinished = true)
                        .Start();
                    return new Flow()
                        .Queue(new Echo(testTime / 2f).OnCompleted(() => echo.Stop(true)))
                        .Queue(new Echo(testTime))
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
            Echo echo = null;

            yield return CreateTester()
                .Act(() =>
                {
                    echo = new Echo(testTime / runs)
                        .OnCompleted(() =>
                        {
                            runned++;
                            if (runned < runs)
                            {
                                echo.Reset().Start();
                            }
                        })
                        .Start();
                    return new Echo(testTime).Start();
                })
                .AssertTime(testTime)
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
            const float testTime = 1f;
            Echo echo = null;

            yield return CreateTester()
                .Act(() =>
                {
                    echo = new Echo()
                        .SetTimeout(testTime / 2f);
                    return new Flow()
                        .Queue(echo)
                        .QueueDeferred(() => echo.Reset())
                        .Start();
                })
                .AssertTime(testTime)
                .Assert(() => Assert.AreEqual(PlayState.Finished, echo.PlayState))
                .Run();
        }
    }
}
