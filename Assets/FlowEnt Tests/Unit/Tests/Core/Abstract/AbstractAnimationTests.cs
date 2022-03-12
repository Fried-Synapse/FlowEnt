using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public abstract class AbstractAnimationTests<TAnimation> : AbstractEngineTests
        where TAnimation : AbstractAnimation
    {
        protected abstract TAnimation CreateAnimation(float testTime);

        [UnityTest]
        public IEnumerator Name()
        {
            const float time = 0.1f;
            const string name = "Billy";
            TAnimation animation = default;

            yield return CreateTester()
                .Arrange(() => animation = CreateAnimation(time))
                .Act(() => animation.SetName(name).Start())
                .AssertTime(time)
                .Assert(() => Assert.AreEqual(name, animation.Name))
                .Run();
        }

        [UnityTest]
        public IEnumerator PlayState_Values()
        {
            bool isBuilding = false;
            bool isWaiting = false;
            bool isPlaying = false;
            bool isPaused = false;
            TAnimation animation = null;

            yield return CreateTester()
                .Act(() =>
                {
                    animation = CreateAnimation(TestTime)
                        .SetDelay(HalfTestTime) as TAnimation;

                    isBuilding = animation.PlayState == PlayState.Building;

                    animation.Start();

                    Flow flowControl = new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => isWaiting = animation.PlayState == PlayState.Waiting))
                                    .Queue(new Tween(QuarterTestTime))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => isPlaying = animation.PlayState == PlayState.Playing))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Pause()))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => isPaused = animation.PlayState == PlayState.Paused))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Resume()))
                                    .Start();
                    return animation;
                })
                .AssertTime(DoubleTestTime)
                .Assert(() =>
                {
                    Assert.True(isBuilding);
                    Assert.True(isWaiting);
                    Assert.True(isPlaying);
                    Assert.True(isPaused);
                    Assert.True(animation.PlayState == PlayState.Finished);
                })
                .Run($"Testing different states on {nameof(PlayState_Values)}", DoubleTestTime);
        }

        [UnityTest]
        public IEnumerator Start()
        {
            yield return CreateTester()
                .Act(() => CreateAnimation(TestTime).Start())
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
                    TAnimation animation = CreateAnimation(TestTime);
                    task = animation.StartAsync();
                    return animation;
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
                    TAnimation animation = CreateAnimation(TestTime).Start() as TAnimation;
                    task = animation.AsAsync();
                    return animation;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Start_Started()
        {
            Exception startException = null;
            TAnimation animation = null;

            yield return CreateTester()
                .Act(() =>
                {
                    animation = CreateAnimation(TestTime).Start() as TAnimation;
                    try
                    {
                        animation.Start();
                    }
                    catch (Exception ex)
                    {
                        startException = ex;
                    }

                    return animation;
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(startException != null && startException is AnimationException animationException && animationException.Animation is TAnimation);
                    Assert.Throws<AnimationException>(() => animation.Start());
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator PauseResume()
        {
            Stopwatch stopwatch = new Stopwatch();

            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(HalfTestTime).Start() as TAnimation;
                    Flow flowControl = new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Pause()))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Resume()))
                                    .Start();
                    return animation;
                })
                .AssertTime(ThreeQuartersTestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop()
        {
            Stopwatch stopwatch = new Stopwatch();
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(HalfTestTime)
                                    .OnCompleted(() => hasFinished = true)
                                    .Start() as TAnimation;

                    return new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Stop()))
                                    .Queue(new Tween(QuarterTestTime))
                                    .Start();
                })
                .Assert(() => Assert.AreEqual(false, hasFinished))
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop_TriggerOnCompleted()
        {
            Stopwatch stopwatch = new Stopwatch();
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(HalfTestTime)
                                        .OnCompleted(() => hasFinished = true)
                                        .Start() as TAnimation;

                    return new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Stop(true)))
                                    .Queue(new Tween(QuarterTestTime))
                                    .Start();
                })
                .Assert(() => Assert.AreEqual(true, hasFinished))
                .Run();
        }

        [UnityTest]
        public IEnumerator Reset()
        {
            const int runs = 3;
            int ran = 0;
            TAnimation animation = null;

            yield return CreateTester()
                .Act(() =>
                {
                    animation = CreateAnimation(TestTime / runs)
                            .OnCompleted(() =>
                            {
                                ran++;
                                if (ran < runs)
                                {
                                    animation.Reset().Start();
                                }
                            })
                            .Start() as TAnimation;
                    return new Tween(TestTime).Start();
                })
                .SetAssertDelay(2)
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(PlayState.Finished, animation.PlayState);
                    Assert.AreEqual(runs, ran);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator ResetUsingFlow()
        {
            TAnimation animation = null;

            yield return CreateTester()
                .Act(() =>
                {
                    animation = CreateAnimation(TestTime / 2f);
                    return new Flow()
                        .Queue(animation)
                        .QueueDeferred(() => animation.Reset())
                        .Start();
                })
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(PlayState.Finished, animation.PlayState))
                .Run();
        }
    }
}
