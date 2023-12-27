using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class ValueTests : AbstractEngineTests
    {
        public override void CreateObjects(int count)
        {
        }

        [UnityTest]
        public IEnumerator FloatValue()
        {
            const float from = 2f;
            const float to = 5f;
            List<float> values = new();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        Assert.True(values.TrueForAll(v => from <= v && v <= to));
                        Assert.AreEqual(from, values[0]);
                        Assert.AreEqual(to, values[^1]);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator IntValue()
        {
            const int from = 2;
            const int to = 5;
            List<int> values = new();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        Assert.True(values.TrueForAll(v => from <= v && v <= to));
                        Assert.AreEqual(from, values[0]);
                        Assert.AreEqual(to, values[^1]);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator Vector2Value()
        {
            Vector2 from = new(2f, 2f);
            Vector2 to = new(5f, 5f);
            List<Vector2> values = new();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        Assert.True(values.TrueForAll(v => from.x <= v.x && v.x <= to.x));
                        Assert.AreEqual(from, values[0]);
                        Assert.AreEqual(to, values[^1]);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator Vector3Value()
        {
            Vector3 from = new(2f, 2f, 2f);
            Vector3 to = new(5f, 5f, 5f);
            List<Vector3> values = new();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        Assert.True(values.TrueForAll(v => from.x <= v.x && v.x <= to.x));
                        Assert.AreEqual(from, values[0]);
                        Assert.AreEqual(to, values[^1]);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator Vector4Value()
        {
            Vector4 from = new(2f, 2f, 2f, 2f);
            Vector4 to = new(5f, 5f, 5f, 5f);
            List<Vector4> values = new();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        Assert.True(values.TrueForAll(v => from.x <= v.x && v.x <= to.x));
                        Assert.AreEqual(from, values[0]);
                        Assert.AreEqual(to, values[^1]);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator AnimationCurveValue()
        {
            AnimationCurve animationCurve = Variables.AnimationCurve;
            Dictionary<float, float> values = new();
            float lastKey = 0;

            yield return CreateTester()
                .Act(() => new Tween(TestTime)
                    .OnUpdating(t => lastKey = t)
                    .Value(animationCurve, (value) => values.Add(lastKey, value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.True(values.ToList().TrueForAll(v => animationCurve.Evaluate(v.Key) == v.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator AnimationCurve2dValue()
        {
            AnimationCurve2d animationCurve = Variables.AnimationCurve;
            Dictionary<float, Vector2> values = new();
            float lastKey = 0;

            yield return CreateTester()
                .Act(() => new Tween(TestTime)
                    .OnUpdating(t => lastKey = t)
                    .Value(animationCurve, (value) => values.Add(lastKey, value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.True(values.ToList().TrueForAll(v => animationCurve.Evaluate(v.Key) == v.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator AnimationCurve3dValue()
        {
            AnimationCurve3d animationCurve = Variables.AnimationCurve;
            Dictionary<float, Vector3> values = new();
            float lastKey = 0;

            yield return CreateTester()
                .Act(() => new Tween(TestTime)
                    .OnUpdating(t => lastKey = t)
                    .Value(animationCurve, (value) => values.Add(lastKey, value))
                    .Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.True(values.ToList().TrueForAll(v => animationCurve.Evaluate(v.Key) == v.Value)))
                .Run();
        }

        [UnityTest]
        public IEnumerator SplineValue()
        {
            Vector3 from = new Vector3(2f, 2f, 2f);
            Vector3 mid = new Vector3(4f, 2f, 4f);
            Vector3 to = new Vector3(5f, 5f, 5f);
            ICurve curve = new BSpline(from, mid, to);
            List<Vector3> values = new List<Vector3>();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        Assert.True(values.TrueForAll(v => from.x <= v.x && v.x <= to.x));
                        Assert.AreEqual(from, values[0]);
                        Assert.AreEqual(to, values[^1]);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator QuaternionValue()
        {
            Quaternion from = Quaternion.Euler(0f, 0f, 0f);
            Quaternion to = Quaternion.Euler(0f, 90f, 0f);
            List<Quaternion> values = new List<Quaternion>();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        Assert.True(values.TrueForAll(v => from.eulerAngles.y <= v.eulerAngles.y && v.eulerAngles.y <= to.eulerAngles.y));
                        FlowEntAssert.AreEqual(from, values[0]);
                        FlowEntAssert.AreEqual(to, values[^1]);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColorValue()
        {
            Color from = Color.blue;
            Color to = Color.red;
            List<Color> values = new List<Color>();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        Assert.True(values.TrueForAll(v => from.r <= v.r && v.r <= to.r));
                        Assert.AreEqual(from, values[0]);
                        Assert.AreEqual(to, values[^1]);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator Color32Value()
        {
            Color32 from = Color.blue;
            Color32 to = Color.red;
            List<Color32> values = new List<Color32>();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        Assert.True(values.TrueForAll(v => from.r <= v.r && v.r <= to.r));
                        Assert.AreEqual(from, values[0]);
                        Assert.AreEqual(to, values[^1]);
                    })
                .Run();
        }
    }
}
