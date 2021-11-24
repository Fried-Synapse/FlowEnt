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
        protected int CompletedFlows { get; set; } = 0;

        protected virtual IEnumerator CreateObjects(int count, int innerCount)
        {
            Vector3 scale = Vector3.one * 0.9f;

            for (int i = 0; i < count; i++)
            {
                List<GameObject> innerGameObjects = new List<GameObject>();
                GameObject parent = new GameObject($"Parent {i}");
                parent.transform.position = new Vector3(0, 0, i);

                for (int j = 0; j < innerCount; j++)
                {
                    GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    gameObject.transform.parent = parent.transform;
                    gameObject.name = $"Test game object {i}-{j}";
                    gameObject.transform.localPosition = new Vector3(j, 0, 0);
                    gameObject.transform.localScale = scale;
                    innerGameObjects.Add(gameObject);
                }
                GameObjects.Add(innerGameObjects);
            }
            yield return null;
        }

        protected void OnFlowComplete()
            => CompletedFlows++;

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
            CompletedFlows = 0;
            System.GC.Collect();
        }
    }
}