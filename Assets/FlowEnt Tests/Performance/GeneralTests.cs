//#define ProperTests
using System;
using System.Collections;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;

#if ProperTests
using System.Collections.Generic;
using UnityEngine;
#endif

namespace FriedSynapse.FlowEnt.Tests.Performance
{
    public class GeneralTests : AbstractTest
    {
        private const string ObjectCreationName = "Object Creation";
        private const string AnimationCreationName = "Animation Creation";
        private const string UsageName = "Usage";
        private const float TestLength = 1f;

#if ProperTests
        private List<Vector3> SplinePoints { get; } = new List<Vector3>()
        {
            new Vector3(0,0,0),
            new Vector3(0,2,0),
            new Vector3(0,3,3),
            new Vector3(5,4,3),
            new Vector3(0,6,0),
        };
#endif

#pragma warning disable IDE0052, RCS1213
#if ProperTests
        private static readonly (int, float)[] emptyTweenParams = new (int, float)[] { (64000, 110f), (128000, 90f), (256000, 60f) };
#else
        private static readonly (int, float)[] emptyTweenParams = new (int, float)[] { (32000, 100f), (64000, 80f), (128000, 40f) };
#endif
#pragma warning restore IDE0052, RCS1213
        [UnityTest, Performance]
        public IEnumerator EmptyTween([ValueSource(nameof(emptyTweenParams))] (int Count, float Fps) data)
        {
            void aniamtionCreation()
            {
                for (int i = 0; i < data.Count; i++)
                {
                    new Tween()
                        .SetTime(TestLength)
                        .OnCompleted(OnAnimationComplete)
                        .Start();
                }
            }

            yield return CreateAndPlay(data.Count, aniamtionCreation);
            AssertPerformance(UsageName, data.Fps);
        }

#if ProperTests

        private static readonly (int, float)[] basicTweenParams = new (int, float)[] { (2000, 100f), (4000, 80f), (8000, 40f) };
        [UnityTest, Performance]
        public IEnumerator BasicTween([ValueSource(nameof(basicTweenParams))] (int Count, float Fps) data)
        {
            yield return CreateObjects(data.Count);

            void aniamtionCreation()
            {
                for (int i = 0; i < data.Count; i++)
                {
                    GameObject gameObject = GameObjects[i];
                    gameObject.transform
                        .Tween(TestLength)
                            .MoveLocalTo(Vector3.one)
                        .Tween
                        .OnCompleted(OnAnimationComplete)
                        .Start();
                }
            }

            yield return CreateAndPlay(data.Count, aniamtionCreation);
            AssertPerformance(UsageName, data.Fps);
        }

        private static readonly (int, float)[] basicTweenBezierParams = new (int, float)[] { (2000, 100f), (4000, 80f), (8000, 40f) };
        [UnityTest, Performance]
        public IEnumerator BasicTweenBezier([ValueSource(nameof(basicTweenBezierParams))] (int Count, float Fps) data)
        {
            yield return CreateObjects(data.Count);

            void aniamtionCreation()
            {
                for (int i = 0; i < data.Count; i++)
                {
                    GameObject gameObject = GameObjects[i];
                    gameObject.transform
                        .Tween(TestLength)
                            .MoveLocalTo(new BSpline(SplinePoints))
                        .Tween
                        .OnCompleted(OnAnimationComplete)
                        .Start();
                }
            }

            yield return CreateAndPlay(data.Count, aniamtionCreation);
            AssertPerformance(UsageName, data.Fps);
        }

#endif

        protected override IEnumerator CreateObjects(int count)
        {
            using (Measure.Frames().WarmupCount(10).Scope(ObjectCreationName))
            {
                yield return base.CreateObjects(count);
            }
        }

        private IEnumerator CreateAndPlay(int count, Action aniamtionCreation)
        {
            using (Measure.Frames().WarmupCount(60).Scope(AnimationCreationName))
            {
                aniamtionCreation();
                yield return null;
            }

            using (Measure.Frames().Scope($"{UsageName} - Start"))
            {
                yield return null;
            }

            using (Measure.Frames().Scope(UsageName))
            {
                while (CompletedAnimations < count)
                {
                    yield return null;
                }
            }
        }

        private void AssertPerformance(string sampleName, float minFps)
        {
            PerformanceTest info = PerformanceTest.Active;
            info.CalculateStatisticalValues();
            SampleGroup sampleGroup = info.SampleGroups.Find(s => s.Name == sampleName);
            double fps = 1000f / sampleGroup.Average;
            Assert.GreaterOrEqual(fps, minFps);
        }
    }
}