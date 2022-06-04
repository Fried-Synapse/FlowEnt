using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FriedSynapse.FlowEnt.Tests.Performance
{
    public class AbstractTest
    {
        protected List<List<GameObject>> GameObjects { get; set; } = new List<List<GameObject>>();
        protected int CompletedAnimations { get; set; } = 0;

        protected virtual IEnumerator CreateObjects(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject gameObject = new GameObject($"Test game object {i}");
                gameObject.transform.position = new Vector3(0, 0, i);
            }
            yield return null;
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

            foreach (List<GameObject> gameObjects in GameObjects)
            {
                foreach (GameObject gameObject in gameObjects)
                {
                    Object.Destroy(gameObject);
                }
            }
            GameObjects = new List<List<GameObject>>();
            CompletedAnimations = 0;
            System.GC.Collect();
        }
    }
}