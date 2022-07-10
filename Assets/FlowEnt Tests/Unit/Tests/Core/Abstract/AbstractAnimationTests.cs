using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public static class AbstractAnimationTestsValues
    {
        public readonly static bool[] stopValues = new bool[] { false, true };
    }

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
#pragma warning disable RCS1047
        public IEnumerator StartAsync()
#pragma warning restore RCS1047
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
        public IEnumerator StartAsync_Cancel()
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
                    CancellationTokenSource source = new CancellationTokenSource();
                    source.CancelAfter((int)TimeSpan.FromSeconds(HalfTestTime).TotalMilliseconds);
                    TAnimation animation = CreateAnimation(TestTime);
                    task = animation.StartAsync(source.Token);
                    return animation;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(HalfTestTime)
                .Run();
        }

        [UnityTest]
#pragma warning disable RCS1047
        public IEnumerator AsAsync()
#pragma warning restore RCS1047
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
        public IEnumerator AsAsync_Cancel()
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
                    CancellationTokenSource source = new CancellationTokenSource();
                    source.CancelAfter((int)TimeSpan.FromSeconds(HalfTestTime).TotalMilliseconds);
                    TAnimation animation = CreateAnimation(TestTime).Start() as TAnimation;
                    task = animation.AsAsync(source.Token);
                    return animation;
                })
                .SetCustomWaiter(customWaiter)
                .AssertTime(HalfTestTime)
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
        public IEnumerator PauseWhileWaiting()
        {
            Stopwatch stopwatch = new Stopwatch();

            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(QuarterTestTime).SetDelay(HalfTestTime).Start() as TAnimation;
                    Flow flowControl = new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Pause()))
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Resume()))
                                    .Start();
                    return animation;
                })
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop([ValueSource(typeof(AbstractAnimationTestsValues), nameof(AbstractAnimationTestsValues.stopValues))] bool triggerOnCompleted)
        {
            TAnimation animation = null;
            Stopwatch stopwatch = new Stopwatch();
            bool hasFinished = false;

            yield return CreateTester()
                .Act(() =>
                {
                    animation = CreateAnimation(HalfTestTime)
                                   .OnCompleted(() => hasFinished = true) as TAnimation;
                    _ = animation.StartAsync();

                    return new Flow()
                                    .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Stop(triggerOnCompleted)))
                                    .Queue(new Tween(QuarterTestTime))
                                    .Start();
                })
                .Assert(() =>
                {
                    Assert.AreEqual(triggerOnCompleted, hasFinished);
                    Assert.DoesNotThrow(() => animation.Stop(triggerOnCompleted));
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator Reset()
        {
            const int runs = 3;
            int ran = 0;
            TAnimation animation = null;

            yield return CreateTester()
                .Arrange(() => animation = (TAnimation)CreateAnimation(TestTime / runs).Start())
                .Act(() =>
                {
                    animation
                        .Stop()
                        .Reset()
                        .OnCompleted(() =>
                        {
                            animation.Reset();
                            ran++;
                            if (ran < runs)
                            {
                                animation.Start();
                            }
                        })
                        .Start();
                    return new Tween(TestTime).Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(PlayState.Building, animation.PlayState);
                    Assert.AreEqual(null, animation.Overdraft);
                    Assert.AreEqual(runs, ran);
                })
                .Run();
        }
    }
}
