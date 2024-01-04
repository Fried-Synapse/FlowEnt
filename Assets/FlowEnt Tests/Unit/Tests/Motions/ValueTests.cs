using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
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
                        values.Should().AllSatisfy(v => v.Should().BeGreaterOrEqualTo(from).And.BeLessOrEqualTo(to));
                        values[0].Should().Be(from);
                        values[^1].Should().Be(to);
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
                        values.Should().AllSatisfy(v => v.Should().BeGreaterOrEqualTo(from).And.BeLessOrEqualTo(to));
                        values[0].Should().Be(from);
                        values[^1].Should().Be(to);
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
                        values.Should().AllSatisfy(v =>
                        {
                            v.x.Should().BeGreaterOrEqualTo(from.x).And.BeLessOrEqualTo(to.x);
                            v.y.Should().BeGreaterOrEqualTo(from.y).And.BeLessOrEqualTo(to.y);
                        });
                        values[0].Should().Be(from);
                        values[^1].Should().Be(to);
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
                        values.Should().AllSatisfy(v =>
                        {
                            v.x.Should().BeGreaterOrEqualTo(from.x).And.BeLessOrEqualTo(to.x);
                            v.y.Should().BeGreaterOrEqualTo(from.y).And.BeLessOrEqualTo(to.y);
                            v.z.Should().BeGreaterOrEqualTo(from.z).And.BeLessOrEqualTo(to.z);
                        });
                        values[0].Should().Be(from);
                        values[^1].Should().Be(to);
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
                        values.Should().AllSatisfy(v =>
                        {
                            v.x.Should().BeGreaterOrEqualTo(from.x).And.BeLessOrEqualTo(to.x);
                            v.y.Should().BeGreaterOrEqualTo(from.y).And.BeLessOrEqualTo(to.y);
                            v.z.Should().BeGreaterOrEqualTo(from.z).And.BeLessOrEqualTo(to.z);
                            v.w.Should().BeGreaterOrEqualTo(from.w).And.BeLessOrEqualTo(to.w);
                        });
                        values[0].Should().Be(from);
                        values[^1].Should().Be(to);
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
                .Assert(() => values.Should().AllSatisfy(v => v.Value.Should().Be(animationCurve.Evaluate(v.Key))))
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
                .Assert(() => values.Should().AllSatisfy(v => v.Value.Should().Be(animationCurve.Evaluate(v.Key))))
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
                .Assert(() => values.Should().AllSatisfy(v => v.Value.Should().Be(animationCurve.Evaluate(v.Key))))
                .Run();
        }

        [UnityTest]
        public IEnumerator CurveValue()
        {
            Vector3 from = new(2f, 2f, 2f);
            Vector3 mid = new(4f, 3f, 4f);
            Vector3 to = new(5f, 5f, 5f);
            ICurve curve = new BSpline(from, mid, to);
            List<Vector3> values = new();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(curve, value => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        values.Should().AllSatisfy(v =>
                        {
                            v.x.Should().BeGreaterOrEqualTo(from.x).And.BeLessOrEqualTo(to.x);
                            v.y.Should().BeGreaterOrEqualTo(from.y).And.BeLessOrEqualTo(to.y);
                            v.z.Should().BeGreaterOrEqualTo(from.z).And.BeLessOrEqualTo(to.z);
                        });
                        values[0].Should().Be(from);
                        values[^1].Should().Be(to);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator QuaternionValue()
        {
            Quaternion from = Quaternion.Euler(0f, 0f, 0f);
            Quaternion to = Quaternion.Euler(0f, 90f, 0f);
            List<Quaternion> values = new();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        values.Should().AllSatisfy(v =>
                        {
                            v.eulerAngles.x.Should().BeGreaterOrEqualTo(from.eulerAngles.x).And.BeLessOrEqualTo(to.eulerAngles.x);
                            v.eulerAngles.y.Should().BeGreaterOrEqualTo(from.eulerAngles.y).And.BeLessOrEqualTo(to.eulerAngles.y);
                            v.eulerAngles.z.Should().BeGreaterOrEqualTo(from.eulerAngles.z).And.BeLessOrEqualTo(to.eulerAngles.z);
                        });
                        values[0].Should().Be(from);
                        values[^1].Should().Be(to);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator ColorValue()
        {
            Color from = Color.clear;
            Color to = Color.white;
            List<Color> values = new();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        values.Should().AllSatisfy(v =>
                        {
                            v.r.Should().BeGreaterOrEqualTo(from.r).And.BeLessOrEqualTo(to.r);
                            v.g.Should().BeGreaterOrEqualTo(from.g).And.BeLessOrEqualTo(to.g);
                            v.b.Should().BeGreaterOrEqualTo(from.b).And.BeLessOrEqualTo(to.b);
                            v.a.Should().BeGreaterOrEqualTo(from.a).And.BeLessOrEqualTo(to.a);
                        });
                        values[0].Should().Be(from);
                        values[^1].Should().Be(to);
                    })
                .Run();
        }

        [UnityTest]
        public IEnumerator Color32Value()
        {
            Color32 from = Color.clear;
            Color32 to = Color.white;
            List<Color32> values = new();

            yield return CreateTester()
                .Act(() => new Tween(TestTime).Value(from, to, (value) => values.Add(value)).Start())
                .AssertTime(TestTime)
                .Assert(
                    () =>
                    {
                        values.Should().AllSatisfy(v =>
                        {
                            v.r.Should().BeGreaterOrEqualTo(from.r).And.BeLessOrEqualTo(to.r);
                            v.g.Should().BeGreaterOrEqualTo(from.g).And.BeLessOrEqualTo(to.g);
                            v.b.Should().BeGreaterOrEqualTo(from.b).And.BeLessOrEqualTo(to.b);
                            v.a.Should().BeGreaterOrEqualTo(from.a).And.BeLessOrEqualTo(to.a);
                        });
                        values[0].Should().Be(from);
                        values[^1].Should().Be(to);
                    })
                .Run();
        }
    }
}