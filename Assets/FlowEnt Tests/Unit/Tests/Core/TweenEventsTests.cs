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

        protected override float GetTotalTimeFromUpdate(float t, float previousValue, float loopTime)
            => previousValue + (t * loopTime - (previousValue % loopTime));

        #region Builder

        [UnityTest]
        public IEnumerator Builder()
        {
            TweenEvents tweenEvents = default;

            yield return CreateTester()
                .Act(() => tweenEvents = Variables.Tween.Events.Build())
                .Assert(() =>
                {
                    Assert(Variables.Tween.Events.OnStarting, tweenEvents.OnStartingEvent);
                    Assert(Variables.Tween.Events.OnStarted, tweenEvents.OnStartedEvent);
                    Assert(Variables.Tween.Events.OnUpdating, tweenEvents.OnUpdatingEvent);
                    Assert(Variables.Tween.Events.OnUpdated, tweenEvents.OnUpdatedEvent);
                    Assert(Variables.Tween.Events.OnLoopCompleted, tweenEvents.OnLoopCompletedEvent);
                    Assert(Variables.Tween.Events.OnCompleted, tweenEvents.OnCompletedEvent);
                    Assert(Variables.Tween.Events.OnCompleting, tweenEvents.OnCompletingEvent);
                })
                .Run();
        }

        #endregion
    }
}