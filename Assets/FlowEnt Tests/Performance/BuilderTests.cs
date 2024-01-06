using System.Collections;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Tween.Transforms;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine.TestTools;
using UnityEngine;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;

namespace FriedSynapse.FlowEnt.Tests.Performance
{
    public class BuilderTests : AbstractTest
    {
        protected override string ObjectCreationName => "Object Creation";
        protected override string AnimationCreationName => "Animation Creation";
        protected override string UsageName => "Usage";

        private static readonly (int, float)[] builtTweenParams = { (2000, 100f), (4000, 80f), (8000, 40f) };

        [UnityTest, Performance]
        public IEnumerator BuiltTween([ValueSource(nameof(builtTweenParams))] (int Count, float Fps) data)
        {
            TweenAuthoring[] tweenAuthorings = new TweenAuthoring[data.Count];

            GameObject createGameObject(int i)
            {
                GameObject gameObject = new GameObject($"Test game object {i}");
                gameObject.SetActive(false);
                gameObject.transform.position = new Vector3(0, 0, i);

                TweenAuthoring tweenAuthoring = gameObject.AddComponent<TweenAuthoring>();
                tweenAuthorings[i] = tweenAuthoring;
                new SerializedObject(tweenAuthoring).Update();

                tweenAuthoring.SetFieldValue("startMode", AbstractAuthoring.StartModeEnum.Custom);
                tweenAuthoring.AnimationBuilder.Options.SetFieldValue("time", TestLength);
                MoveVectorMotion.ValueBuilder motion = new MoveVectorMotion.ValueBuilder();
                motion.SetFieldValue("item", gameObject.transform);
                motion.SetFieldValue("value", Vector3.one);
                tweenAuthoring.AnimationBuilder.Motions
                    .SetFieldValue("items", new List<AbstractTweenMotionBuilder> { motion });
                gameObject.SetActive(true);

                return gameObject;
            }

            yield return CreateObjects(data.Count, createGameObject);

            void animationCreation()
            {
                for (int i = 0; i < data.Count; i++)
                {
                    tweenAuthorings[i].StartCustomMode();
                    tweenAuthorings[i].Animation.OnCompleted(OnAnimationComplete);
                }
            }

            yield return CreateAndPlay(data.Count, animationCreation);
            AssertPerformance(UsageName, data.Fps);
        }
    }
}