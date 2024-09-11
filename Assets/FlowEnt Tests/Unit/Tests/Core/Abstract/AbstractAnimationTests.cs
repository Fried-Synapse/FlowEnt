using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public static partial class AbstractAnimationTestsValues
    {
        public static readonly bool[] stopValues = { false, true };
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
                .Assert(() => animation.Name.Should().Be(name))
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

                    new Flow()
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
                    isBuilding.Should().BeTrue();
                    isWaiting.Should().BeTrue();
                    isPlaying.Should().BeTrue();
                    isPaused.Should().BeTrue();
                    animation.PlayState.Should().Be(PlayState.Finished);
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
        public IEnumerator AsAsync()
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
                    startException.Should().NotBeNull()
                        .And.BeOfType<AnimationException>()
                        .Which.Animation.Should().BeOfType<TAnimation>();
                    Action act = () => animation.Start();
                    act.Should().Throw<AnimationException>();
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator PauseResume()
        {
            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(HalfTestTime).Start() as TAnimation;
                    new Flow()
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
            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(QuarterTestTime).SetDelay(HalfTestTime).Start() as TAnimation;
                    new Flow()
                        .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Pause()))
                        .Queue(new Tween(QuarterTestTime).OnCompleted(() => animation.Resume()))
                        .Start();
                    return animation;
                })
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Stop(
            [ValueSource(typeof(AbstractAnimationTestsValues), nameof(AbstractAnimationTestsValues.stopValues))]
            bool triggerOnCompleted)
        {
            TAnimation animation = null;
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
                    hasFinished.Should().Be(triggerOnCompleted);
                    Action act = () => animation.Stop(triggerOnCompleted);
                    act.Should().NotThrow();
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
                    return new Tween(TestTime * 1.1f).Start();
                })
                .Assert(() =>
                {
                    animation.PlayState.Should().Be(PlayState.Building);
                    animation.Overdraft.Should().Be(null);
                    ran.Should().Be(runs);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator Overdraft()
        {
            const float time = 0.05f;
            const int tweens = 100;
            const float testTime = time * tweens;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new();
                    for (int j = 0; j < tweens; j++)
                    {
                        flow.Queue(CreateAnimation(time));
                    }

                    return flow.Start();
                })
                .AssertTime(testTime)
                .Run($"Testing Overdraft for animation.", testTime);
        }

        #region IControllable

        [UnityTest]
        public IEnumerator IsSeekable()
        {
            TAnimation animation = null;

            yield return CreateTester()
                .Act(() =>
                {
                    animation = CreateAnimation(TestTime).Start() as TAnimation;
                    return animation;
                })
                .Assert(() => animation.Controllable.IsSeekable.Should().Be(typeof(TAnimation) != typeof(Flow)))
                .Run();
        }

        [UnityTest]
        public IEnumerator SeekRatio()
        {
            if (typeof(TAnimation) == typeof(Flow))
            {
                //Flows can't be seeked
                yield break;
            }

            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(TestTime).Start() as TAnimation;
                    animation.Controllable.SeekRatio = 0.5f;
                    return animation;
                })
                .AssertTime(HalfTestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator SimulateUpdate()
        {
            yield return CreateTester()
                .Act(() =>
                {
                    TAnimation animation = CreateAnimation(TestTime).Start() as TAnimation;
                    animation.Controllable.SimulateUpdate(HalfTestTime);
                    return animation;
                })
                .AssertTime(HalfTestTime)
                .Run();
        }

        #endregion
    }
}