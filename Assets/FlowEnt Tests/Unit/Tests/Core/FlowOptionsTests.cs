using System;
using System.Collections;
using FluentAssertions;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class FlowOptionsTests : AbstractAnimationOptionsTests<Flow, FlowOptions>
    {
        protected override Flow CreateAnimation(float testTime)
            => new Flow().Queue(new Tween(testTime));

        protected override Flow CreateAnimation(float testTime, FlowOptions options)
            => new Flow().SetOptions(options).Queue(new Tween(testTime));

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            FlowOptions flowOptions = default;

            yield return CreateTester()
                .Act(() => flowOptions = Variables.Flow.Options.Build())
                .Assert(() =>
                {
                    flowOptions.Name.Should().Be(Variables.Flow.Options.Name);
                    flowOptions.UpdateType.Should().Be(Variables.Flow.Options.UpdateType);
                    flowOptions.AutoStart.Should().Be(false);
                    flowOptions.SkipFrames.Should().Be(Variables.Flow.Options.SkipFrames);
                    flowOptions.Delay.Should().Be(Variables.Flow.Options.Delay);
                    flowOptions.TimeScale.Should().Be(Variables.Flow.Options.TimeScale);
                    flowOptions.LoopCount.Should().Be(Variables.Flow.Options.LoopCount);
                })
                .Run();
        }

        #endregion

        #region Time

        private IEnumerator Time_ForAnimation(Func<float, AbstractAnimation> createAnimation, string name)
        {
            const float time = 0.05f;
            const int tweens = 10;
            const int innerFlows = 10;
            const float testTime = time * tweens * innerFlows;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow();

                    for (int i = 0; i < innerFlows; i++)
                    {
                        Flow innerFlow = new Flow();
                        flow.Queue(innerFlow);
                        for (int j = 0; j < tweens; j++)
                        {
                            innerFlow.Queue(createAnimation(time));
                        }
                    }

                    return flow.Start();
                })
                .AssertTime(testTime)
                .Run($"Animations inside flows inside flow on {name}", testTime);
        }

        [UnityTest]
        public IEnumerator Time_ForTween()
            => Time_ForAnimation((time) => new Tween(time), nameof(Time_ForTween));

        [UnityTest]
        public IEnumerator Timeout_ForEchoes()
            => Time_ForAnimation((time) => new Echo(time), nameof(Timeout_ForEchoes));

        #endregion

        #region LoopCount

        private IEnumerator LoopCount_ForAnimation(Func<float, AbstractAnimation> createAnimation, string name)
        {
            const int tweens = 3;
            const int loopCount = 3;
            const float testTime = QuarterTestTime * tweens * loopCount;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new();

                    for (int i = 0; i < tweens; i++)
                    {
                        flow.Queue(createAnimation(QuarterTestTime).SetLoopCount(loopCount));
                    }

                    return flow.Start();
                })
                .AssertTime(testTime)
                .Run($"Loop inside queued animations on {name}", testTime);
        }

        [UnityTest]
        public IEnumerator LoopCount_ForTween()
            => LoopCount_ForAnimation((time) => new Tween(time), nameof(LoopCount_ForTween));

        [UnityTest]
        public IEnumerator LoopCount_ForEchoes()
            => LoopCount_ForAnimation((time) => new Echo(time), nameof(LoopCount_ForEchoes));

        #endregion

        #region TimeScale

        private IEnumerator TimeScale_ForAnimation(Func<float, AbstractAnimation> createAnimation)
        {
            const float timeScale = 2f;

            yield return CreateTester()
                .Act(() => new Flow()
                    .Queue(createAnimation(TestTime).SetTimeScale(timeScale))
                    .Start())
                .AssertTime(TestTime / timeScale)
                .Run();
        }

        [UnityTest]
        public IEnumerator TimeScale_ForTween()
            => TimeScale_ForAnimation((time) => new Tween(time));

        [UnityTest]
        public IEnumerator TimeScale_ForEchoes()
            => TimeScale_ForAnimation((time) => new Echo(time));

        #endregion

        #region SkipFrames

        private IEnumerator SkipFrames_ForAnimation(Func<float, AbstractAnimation> createAnimation)
        {
            const int skipFrames = 20;
            int frameCountStart = 0;
            int frameCount = 0;

            yield return CreateTester()
                .Arrange(() => frameCountStart = Time.frameCount)
                .Act(() => new Flow()
                            .Queue(createAnimation(HalfTestTime)
                                .SetSkipFrames(skipFrames)
                                .OnStarted(() => frameCount = Time.frameCount - frameCountStart))
                            .Start())
                .Assert(() => frameCount.Should().Be(skipFrames))
                .Run();
        }

        [UnityTest]
        public IEnumerator SkipFrames_ForTween()
            => SkipFrames_ForAnimation((time) => new Tween(time));

        [UnityTest]
        public IEnumerator SkipFrames_ForEchoes()
            => SkipFrames_ForAnimation((time) => new Echo(time));

        #endregion

        #region Delay

        public IEnumerator Delay_ForAnimation(Func<float, AbstractAnimation> createAnimation)
        {
            yield return CreateTester()
                .Act(() => new Flow()
                            .Queue(createAnimation(HalfTestTime).SetDelay(HalfTestTime))
                            .Start())
                .AssertTime(TestTime)
                .Run();
        }

        [UnityTest]
        public IEnumerator Delay_ForTween()
            => Delay_ForAnimation((time) => new Tween(time));

        [UnityTest]
        public IEnumerator Delay_ForEchoes()
            => Delay_ForAnimation((time) => new Echo(time));

        #endregion
    }
}