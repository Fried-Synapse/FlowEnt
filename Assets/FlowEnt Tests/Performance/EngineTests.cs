using System.Collections;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Performance
{
    public class EngineTests : AbstractTest
    {
        protected override string ObjectCreationName => "Object Creation";
        protected override string AnimationCreationName => "Animation Creation";
        protected override string UsageName => "Usage";

        private static readonly (int, float)[] emptyTweenParams = { (32000, 100f), (64000, 80f), (128000, 40f) };

        [UnityTest, Performance]
        public IEnumerator EmptyTween([ValueSource(nameof(emptyTweenParams))] (int Count, float Fps) data)
        {
            void animationCreation()
            {
                for (int i = 0; i < data.Count; i++)
                {
                    new Tween()
                        .SetTime(TestLength)
                        .OnCompleted(OnAnimationComplete)
                        .Start();
                }
            }

            yield return CreateAndPlay(data.Count, animationCreation);
            AssertPerformance(UsageName, data.Fps);
        }

        private static readonly (int, float)[] basicTweenParams = { (2000, 100f), (4000, 80f), (8000, 40f) };

        [UnityTest, Performance]
        public IEnumerator BasicTween([ValueSource(nameof(basicTweenParams))] (int Count, float Fps) data)
        {
            yield return CreateObjects(data.Count);

            void animationCreation()
            {
                for (int i = 0; i < data.Count; i++)
                {
                    GameObject gameObject = GameObjects[i];
                    gameObject.transform
                        .Tween(TestLength)
                            .MoveLocalTo(Vector3.one)
                        .OnCompleted(OnAnimationComplete)
                        .Start();
                }
            }

            yield return CreateAndPlay(data.Count, animationCreation);
            AssertPerformance(UsageName, data.Fps);
        }
    }
}