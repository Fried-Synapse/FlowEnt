using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Performance
{
    public class GeneralTests : AbstractTest
    {
        private const string ObjectCreationName = "Object Creation";
        private const string FlowCreationName = "Flow Creation";
        private const string UsageName = "Usage";
        private const float TestLength = 1f;

        private List<Vector3> SplinePoints { get; } = new List<Vector3>()
    {
        new Vector3(0,0,0),
        new Vector3(0,2,0),
        new Vector3(0,3,3),
        new Vector3(5,4,3),
        new Vector3(0,6,0),
    };

        private static readonly (int, float)[] emptyTweenValues = new (int, float)[] { (64000, 110f), (128000, 90f), (256000, 60f) };
        [UnityTest, Performance]
        public IEnumerator EmptyTween([ValueSource("emptyTweenValues")] (int Count, float Fps) data)
        {
            void flowCreation()
            {
                for (int i = 0; i < data.Count; i++)
                {
                    new Tween()
                        .SetTime(TestLength)
                        .OnCompleted(OnFlowComplete)
                        .Start();
                }
            }

            yield return CreateFlowsAndPlay(data.Count, flowCreation);
            AssertPerformance(UsageName, data.Fps);
        }

        private static readonly (int, float)[] basicTweenValues = new (int, float)[] { (2000, 100f), (4000, 80f), (8000, 40f) };
        [UnityTest, Performance]
        public IEnumerator BasicTween([ValueSource("basicTweenValues")] (int Count, float Fps) data)
        {
            yield return CreateObjects(data.Count, 1);

            void flowCreation()
            {
                for (int i = 0; i < data.Count; i++)
                {
                    GameObject gameObject = GameObjects[i][0];
                    gameObject.transform
                        .Tween(TestLength)
                            .MoveLocalTo(Vector3.one)
                        .Tween
                        .OnCompleted(OnFlowComplete)
                        .Start();
                }
            }

            yield return CreateFlowsAndPlay(data.Count, flowCreation);
            AssertPerformance(UsageName, data.Fps);
        }

        private static readonly (int, float)[] basicTweenBezierValues = new (int, float)[] { (2000, 100f), (4000, 80f), (8000, 40f) };
        [UnityTest, Performance]
        public IEnumerator BasicTweenBezier([ValueSource("basicTweenBezierValues")] (int Count, float Fps) data)
        {
            yield return CreateObjects(data.Count, 1);

            void flowCreation()
            {
                for (int i = 0; i < data.Count; i++)
                {
                    GameObject gameObject = GameObjects[i][0];
                    gameObject.transform
                        .Tween(TestLength)
                            .MoveLocalTo(new BSpline(SplinePoints))
                        .Tween
                        .OnCompleted(OnFlowComplete)
                        .Start();
                }
            }

            yield return CreateFlowsAndPlay(data.Count, flowCreation);
            AssertPerformance(UsageName, data.Fps);
        }

        protected override IEnumerator CreateObjects(int count, int innerCount)
        {
            using (Measure.Frames().WarmupCount(10).Scope(ObjectCreationName))
            {
                yield return base.CreateObjects(count, innerCount);
            }
        }

        private IEnumerator CreateFlowsAndPlay(int count, Action flowCreation)
        {
            using (Measure.Frames().WarmupCount(60).Scope(FlowCreationName))
            {
                flowCreation();
                yield return null;
            }

            using (Measure.Frames().Scope($"{UsageName} - Start"))
            {
                yield return null;
            }

            using (Measure.Frames().Scope(UsageName))
            {
                while (CompletedFlows < count)
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
            Debug.Log(fps);
            Assert.GreaterOrEqual(fps, minFps);
        }
    }
}