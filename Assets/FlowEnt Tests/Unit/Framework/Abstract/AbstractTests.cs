using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public abstract class AbstractTests
    {
        protected const float TestTime = 0.5f;

        public List<GameObject> GameObjects { get; set; } = new List<GameObject>();
        public GameObject GameObject => GameObjects[0];

        public abstract void CreateObjects(int count);

        protected virtual string SceneName => "UnitTestsScene";

        protected AnimationTester CreateTester(int count = 1)
            => new AnimationTester(this, count);

        protected virtual void OnSetUp()
        {
        }

        [SetUp]
        protected virtual void SetUp()
        {
            SceneManager.LoadScene(SceneName, LoadSceneMode.Single);

            OnSetUp();
        }

        protected virtual void OnTeardown()
        {
        }

        [TearDown]
        protected virtual void Teardown()
        {
            OnTeardown();

            foreach (GameObject gameObject in GameObjects)
            {
                Object.Destroy(gameObject);
            }
            GameObjects = new List<GameObject>();
            GC.Collect();
        }
    }

    public abstract class AbstractTests<TVariables> : AbstractTests
        where TVariables : AbstractVariables
    {
        private readonly Lazy<TVariables> variables = new Lazy<TVariables>(() => Object.FindObjectOfType<TVariables>());
        public TVariables Variables => variables.Value;
    }
}