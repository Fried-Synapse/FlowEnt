using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    [Category("Runtime")]
    public abstract class AbstractTests
    {
        public const float TestTime = 0.3f;
        protected const float DoubleTestTime = TestTime * 2f;
        protected const float TripleTestTime = TestTime * 3f;
        protected const float HalfTestTime = TestTime / 2f;
        protected const float ThirdTestTime = TestTime / 3f;
        protected const float TwoThirdsTestTime = TestTime * 2f / 3f;
        protected const float QuarterTestTime = TestTime / 4f;
        protected const float ThreeQuartersTestTime = TestTime * 3f / 4f;

        public List<GameObject> GameObjects { get; set; } = new();
        public GameObject GameObject => GameObjects[0];

        public abstract void CreateObjects(int count);

        protected virtual string SceneName => "UnitTests";

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
        private TVariables variables;
        protected TVariables Variables => variables ??= Object.FindObjectOfType<TVariables>();

        protected override void OnTeardown()
        {
            base.OnTeardown();
            variables = null;
        }
    }
}