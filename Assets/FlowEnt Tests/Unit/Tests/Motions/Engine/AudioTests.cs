using System.Collections;
using FluentAssertions;
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
                .Assert(() => Component.pitch.Should().Be(value))
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
                .Assert(() => Component.pitch.Should().Be(to))
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
                    startingValue.Should().Be(from);
                    Component.pitch.Should().Be(to);
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
                .Assert(() => Component.volume.Should().Be(value))
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
                .Assert(() => Component.volume.Should().Be(maxValue))
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
                    .OnUpdated((_) => underClamp = underClamp ? underClamp : Component.volume < 0)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Component.volume.Should().Be(minValue);
                    underClamp.Should().BeFalse();
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
                    .OnUpdated((_) => overClamp = overClamp ? overClamp : Component.volume > 1)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Component.volume.Should().Be(maxValue);
                    overClamp.Should().BeFalse();
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
                .Assert(() => Component.volume.Should().Be(minValue))
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
                .Assert(() => Component.volume.Should().Be(to))
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
                .Assert(() => Component.volume.Should().Be(minValue))
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
                    startingValue.Should().Be(from);
                    Component.volume.Should().Be(to);
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
                    startingValue.Should().Be(minValue);
                    Component.volume.Should().Be(maxValue);
                })
                .Run();
        }

        #endregion
    }
}