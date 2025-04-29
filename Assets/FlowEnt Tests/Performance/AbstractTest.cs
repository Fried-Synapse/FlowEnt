using System;
using System.Collections;
using FluentAssertions;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace FriedSynapse.FlowEnt.Tests.Performance
{
    [Category("FlowEnt_Performance")]
    public abstract class AbstractTest
    {
        protected const float TestLength = 1f;
        protected abstract string ObjectCreationName { get; }
        protected abstract string AnimationCreationName { get; }
        protected abstract string UsageName { get; }

        protected GameObject[] GameObjects { get; private set; }
        private int CompletedAnimations { get; set; }


        protected IEnumerator CreateObjects(int count, Func<int, GameObject> onCreating = null)
        {
            using (Measure.Frames().WarmupCount(10).Scope(ObjectCreationName))
            {
                onCreating ??= i =>
                {
                    GameObject gameObject = new GameObject($"Test game object {i}");
                    gameObject.transform.position = new Vector3(0, 0, i);
                    return gameObject;
                };

                GameObjects = new GameObject[count];
                for (int i = 0; i < count; i++)
                {
                    GameObjects[i] = onCreating(i);
                }

                yield return null;
            }
        }

        protected void OnAnimationComplete()
            => CompletedAnimations++;

        protected virtual void OnSetUp()
        {
        }

        [SetUp]
        public void SetUp()
        {
            SceneManager.LoadScene("PerformanceTestsScene", LoadSceneMode.Single);

            OnSetUp();
        }

        protected virtual void OnTeardown()
        {
        }

        [TearDown]
        public void Teardown()
        {
            OnTeardown();

            if (GameObjects != null)
            {
                foreach (GameObject gameObject in GameObjects)
                {
                    Object.Destroy(gameObject);
                }

                GameObjects = null;
            }

            CompletedAnimations = 0;
            GC.Collect();
        }

        protected IEnumerator CreateAndPlay(int count, Action animationCreation)
        {
            using (Measure.Frames().WarmupCount(60).Scope(AnimationCreationName))
            {
                animationCreation();
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

        protected void AssertPerformance(string sampleName, float minFps)
        {
            PerformanceTest info = PerformanceTest.Active;
            info.CalculateStatisticalValues();
            SampleGroup sampleGroup = info.SampleGroups.Find(s => s.Name == sampleName);
            double fps = 1000f / sampleGroup.Average;
            Debug.Log($"FPS results: {fps}.");
            fps.Should().BeGreaterOrEqualTo(minFps);
        }
    }
}