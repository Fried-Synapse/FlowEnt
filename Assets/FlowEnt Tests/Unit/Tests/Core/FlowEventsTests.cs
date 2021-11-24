using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class FlowEventsTests : AbstractEngineTests
    {
        [UnityTest]
        public IEnumerator OnStarted()
        {
            bool wasCalled = false;
            float deltaT = 0;
            float controlT = 0;
            const float expectedT = 0f;

            yield return CreateTester()
                .Act(() =>
                    new Flow()
                        .OnStarted(() => { wasCalled = true; controlT = deltaT; })
                        .Queue(new Tween(TestTime).OnUpdated(t => deltaT = t))
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.AreEqual(expectedT, controlT);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnLoopCompleted()
        {
            const int expectedLoopCount = 2;
            int loopCount = 0;
            float deltaT = 0;
            float controlT = 0;
            const float expectedT = 1f;

            yield return CreateTester()
                .Act(() =>
                    new Flow()
                        .SetLoopCount(expectedLoopCount)
                        .OnLoopCompleted((_) => { loopCount++; controlT = deltaT; })
                        .Queue(new Tween(TestTime / expectedLoopCount).OnUpdated(t => deltaT = t))
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedLoopCount, loopCount);
                    Assert.AreEqual(expectedT, controlT);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnLoopCompleted_Infinite()
        {
            const int expectedLoopCount = 2;
            int loopCount = 0;
            float deltaT = 0;
            float controlT = 0;
            const float expectedT = 1f;

            yield return CreateTester()
                .Act(() =>
                {
                    Flow flow = new Flow()
                        .SetLoopCount(expectedLoopCount)
                        .OnLoopCompleted((_) => { loopCount++; controlT = deltaT; })
                        .Queue(new Tween(TestTime / expectedLoopCount).OnUpdated(t => deltaT = t))
                        .Start();
                    return new Tween(TestTime).OnCompleted(() => flow.Stop()).Start();
                })
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.AreEqual(expectedLoopCount, loopCount);
                    Assert.AreEqual(expectedT, controlT);
                })
                .Run();
        }

        [UnityTest]
        public IEnumerator OnCompleted()
        {
            bool wasCalled = false;
            float deltaT = 0f;
            float controlT = 0f;
            const float expectedT = 1f;

            yield return CreateTester()
                .Act(() =>
                    new Flow()
                        .OnCompleted(() => { wasCalled = true; controlT = deltaT; })
                        .Queue(new Tween(TestTime).OnUpdated(t => deltaT = t))
                        .Start())
                .AssertTime(TestTime)
                .Assert(() =>
                {
                    Assert.True(wasCalled);
                    Assert.AreEqual(expectedT, controlT);
                })
                .Run();
        }
    }
}
