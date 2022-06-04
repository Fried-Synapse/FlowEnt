using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FriedSynapse.FlowEnt.Tests.Performance
{
    public class AbstractTest
    {
        protected GameObject[] GameObjects { get; set; }
        protected int CompletedAnimations { get; set; } = 0;

        protected virtual IEnumerator CreateObjects(int count)
        {
            GameObjects = new GameObject[count];
            for (int i = 0; i < count; i++)
            {
                GameObject gameObject = new GameObject($"Test game object {i}");
                gameObject.transform.position = new Vector3(0, 0, i);
                GameObjects[i] = gameObject;
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

            if (GameObjects != null)
            {
                foreach (GameObject gameObject in GameObjects)
                {
                    Object.Destroy(gameObject);
                }

                GameObjects = null;
            }
            CompletedAnimations = 0;
            System.GC.Collect();
        }
    }
}