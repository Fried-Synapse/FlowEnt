using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Motions
{
    public class AudioTests : AbstractEngineTests
    {
        private AudioSource audioSource;

        private AudioSource AudioSource
        {
            get
            {
                if (audioSource == null)
                {
                    audioSource = GameObject.AddComponent<AudioSource>();
                }
                return audioSource;
            }
        }

        #region Pitch

        [UnityTest]
        public IEnumerator Pitch()
        {
            const float value = 2.3f;

            yield return CreateTester()
                .Arrange(() => AudioSource.pitch = 0f)
                .Act(() => AudioSource.Tween(TestTime).Pitch(value).Start())
                .AssertTime(TestTime)
                .Assert(() => FlowEntAssert.Equal(value, AudioSource.pitch))
                .Run();
        }

        [UnityTest]
        public IEnumerator PitchTo()
        {
            const float to = 2.3f;

            yield return CreateTester()
                .Arrange(() => AudioSource.pitch = 0f)
                .Act(() => AudioSource.Tween(TestTime).PitchTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, AudioSource.pitch))
                .Run();
        }

        [UnityTest]
        public IEnumerator PitchFromTo()
        {
            const float from = -2.3f;
            const float to = 2.3f;

            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => AudioSource.pitch = 0f)
                .Act(() => AudioSource.Tween(TestTime).PitchTo(from, to)
                    .OnUpdated(_ => startingValue ??= AudioSource.pitch)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, AudioSource.pitch);
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
                .Arrange(() => AudioSource.volume = 0f)
                .Act(() => AudioSource.Tween(TestTime).Volume(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(value, AudioSource.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeOverMaxClamp()
        {
            const float value = 1.3f;
            const float maxValue = 1f;

            yield return CreateTester()
                .Arrange(() => AudioSource.volume = 0f)
                .Act(() => AudioSource.Tween(TestTime).Volume(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(maxValue, AudioSource.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeUnderMinClampWithEasing()
        {
            const float value = -1f;
            const float minValue = 0f;

            bool underClamp = false;

            yield return CreateTester()
                .Arrange(() => AudioSource.volume = 0.5f)
                .Act(() => AudioSource.Tween(TestTime)
                    .Volume(value)
                    .SetEasing(Easing.EaseOutBack)
                    .OnUpdated((t) => underClamp = underClamp ? underClamp : AudioSource.volume < 0)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(minValue, AudioSource.volume);
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
                .Arrange(() => AudioSource.volume = 0f)
                .Act(() => AudioSource.Tween(TestTime)
                    .Volume(value)
                    .SetEasing(Easing.EaseOutBack)
                    .OnUpdated((t) => overClamp = overClamp ? overClamp : AudioSource.volume > 1)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(maxValue, AudioSource.volume);
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
                .Arrange(() => AudioSource.volume = 0.5f)
                .Act(() => AudioSource.Tween(TestTime).Volume(value).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(minValue, AudioSource.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeTo()
        {
            const float to = 0.75f;

            yield return CreateTester()
                .Arrange(() => AudioSource.volume = 0f)
                .Act(() => AudioSource.Tween(TestTime).VolumeTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(to, AudioSource.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeToOverMaxClamp()
        {
            const float to = 1.3f;
            const float maxValue = 1f;

            yield return CreateTester()
                .Arrange(() => AudioSource.volume = 0f)
                .Act(() => AudioSource.Tween(TestTime).VolumeTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(maxValue, AudioSource.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeToUnderMinClamp()
        {
            const float to = -0.5f;
            const float minValue = 0f;

            yield return CreateTester()
                .Arrange(() => AudioSource.volume = 0.25f)
                .Act(() => AudioSource.Tween(TestTime).VolumeTo(to).Start())
                .AssertTime(TestTime)
                .Assert(() => Assert.AreEqual(minValue, AudioSource.volume))
                .Run();
        }

        [UnityTest]
        public IEnumerator VolumeFromTo()
        {
            const float from = 0.25f;
            const float to = 0.75f;

            float? startingValue = null;

            yield return CreateTester()
                .Arrange(() => AudioSource.volume = 0f)
                .Act(() => AudioSource.Tween(TestTime).VolumeTo(from, to)
                    .OnUpdated(_ => startingValue ??= AudioSource.volume)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(from, startingValue);
                    Assert.AreEqual(to, AudioSource.volume);
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
                .Arrange(() => AudioSource.volume = 0.5f)
                .Act(() => AudioSource.Tween(TestTime).VolumeTo(from, to)
                    .OnUpdated(_ => startingValue ??= AudioSource.volume)
                    .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(minValue, startingValue);
                    Assert.AreEqual(maxValue, AudioSource.volume);
                })
                .Run();
        }

        #endregion
    }
}
