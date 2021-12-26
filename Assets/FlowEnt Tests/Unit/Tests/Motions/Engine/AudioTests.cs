using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class AudioTests : AbstractEngineTests<AudioSource>
    {
        #region Pitch

        [UnityTest]
        public IEnumerator Pitch()
        {
            const float value = 2.3f;

            yield return CreateTester()
                .Arrange(() => Component.pitch = 0f)
                .Act(() => Component.Tween(TestTime).Pitch(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.Equal(value, Component.pitch))
                .Run();
        }

        [UnityTest]
        public IEnumerator PitchTo()
        {
            const float to = 2.3f;

            yield return CreateTester()
                .Arrange(() => Component.pitch = 0f)
                .Act(() => Component.Tween(TestTime).PitchTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.pitch))
                .Run();
        }

        [UnityTest]
        public IEnumerator PitchFromTo()
        {
            const float from = -2.3f;
            const float to = 2.3f;

            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.pitch = 0f)
                .Act(() => Component.Tween(TestTime).PitchTo(from, to)
                    .OnUpdated(_ => startingValue ??= Component.pitch)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Component.pitch);
                })
                .Run();
        }

        #endregion

        #region Volume


        [UnityTest]
        public IEnumerator Volume()
        {
            const float value = 0.75f;

            yield return CreateTester()
                .Arrange(() => Component.volume = 0f)
                .Act(() => Component.Tween(TestTime).Volume(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, Component.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeOverMaxClamp()
        {
            const float value = 1.3f;
            const float maxValue = 1f;

            yield return CreateTester()
                .Arrange(() => Component.volume = 0f)
                .Act(() => Component.Tween(TestTime).Volume(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(maxValue, Component.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeUnderMinClampWithEasing()
        {
            const float value = -1f;
            const float minValue = 0f;

            bool underClamp = false;

            yield return CreateTester()
                .Arrange(() => Component.volume = 0.5f)
                .Act(() => Component.Tween(TestTime)
                    .Volume(value)
                    .SetEasing(Easing.EaseOutBack)
                    .OnUpdated((t) => underClamp = underClamp ? underClamp : Component.volume < 0)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(minValue, Component.volume);
                    Assert.IsFalse(underClamp);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeOverMaxClampWithEasing()
        {
            const float value = 1f;
            const float maxValue = 1f;

            bool overClamp = false;

            yield return CreateTester()
                .Arrange(() => Component.volume = 0f)
                .Act(() => Component.Tween(TestTime)
                    .Volume(value)
                    .SetEasing(Easing.EaseOutBack)
                    .OnUpdated((t) => overClamp = overClamp ? overClamp : Component.volume > 1)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(maxValue, Component.volume);
                    Assert.IsFalse(overClamp);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeUnderMinClamp()
        {
            const float value = -0.75f;
            const float minValue = 0f;

            yield return CreateTester()
                .Arrange(() => Component.volume = 0.5f)
                .Act(() => Component.Tween(TestTime).Volume(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(minValue, Component.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => Component.volume = 0f)
                .Act(() => Component.Tween(TestTime).VolumeTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, Component.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeToOverMaxClamp()
        {
            const float to = 1.3f;
            const float maxValue = 1f;

            yield return CreateTester()
                .Arrange(() => Component.volume = 0f)
                .Act(() => Component.Tween(TestTime).VolumeTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(maxValue, Component.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeToUnderMinClamp()
        {
            const float to = -0.5f;
            const float minValue = 0f;

            yield return CreateTester()
                .Arrange(() => Component.volume = 0.25f)
                .Act(() => Component.Tween(TestTime).VolumeTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(minValue, Component.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;

            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.volume = 0f)
                .Act(() => Component.Tween(TestTime).VolumeTo(from, to)
                    .OnUpdated(_ => startingValue ??= Component.volume)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, Component.volume);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeFromToWithinClamp()
        {
            const float from = -0.5f;
            const float to = 1.5f;
            const float minValue = 0f;
            const float maxValue = 1f;

            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => Component.volume = 0.5f)
                .Act(() => Component.Tween(TestTime).VolumeTo(from, to)
                    .OnUpdated(_ => startingValue ??= Component.volume)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(minValue, startingValue);
                    Assert.AreEqual(maxValue, Component.volume);
                })
                .Run();
        }

        #endregion
    }
}
