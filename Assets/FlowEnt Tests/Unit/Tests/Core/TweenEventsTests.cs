using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace FriedSynapse.FlowEnt.Tests.Unit.Core
{
    public class TweenEventsTests : AbstractAnimationEventsTests<Tween>
    {
        protected override Tween CreateAnimation(float testTime)
            => new Tween(testTime);

        protected override float GetUnitValue(float currentChange, float previousValue, float fullUnitValue)
            => currentChange;

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            TweenEvents tweenEvents = default;

            yield return CreateTester()
                .Act(() => tweenEvents = Variables.Tween.Events.Build())
                .Assert(() =>
                {
                    static void assert(UnityEventBase unityEvent, Delegate action) => Assert.AreEqual(unityEvent.GetPersistentEventCount() == 0, action == null);

                    assert(Variables.Tween.Events.OnStarting, tweenEvents.OnStartingEvent);
                    assert(Variables.Tween.Events.OnStarted, tweenEvents.OnStartedEvent);
                    assert(Variables.Tween.Events.OnUpdating, tweenEvents.OnUpdatingEvent);
                    assert(Variables.Tween.Events.OnUpdated, tweenEvents.OnUpdatedEvent);
                    assert(Variables.Tween.Events.OnLoopCompleted, tweenEvents.OnLoopCompletedEvent);
                    assert(Variables.Tween.Events.OnCompleted, tweenEvents.OnCompletedEvent);
                    assert(Variables.Tween.Events.OnCompleting, tweenEvents.OnCompletingEvent);
                })
                .Run();
        }

        #endregion
    }
}
