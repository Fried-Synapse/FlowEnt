using System;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class AnimationTester
    {
        private const float OvertimeAllowance = 0.1f;
        private const float MaxRunTime = AbstractTests.TestTime + OvertimeAllowance;

        public AnimationTester(AbstractTests tests, int count)
        {
            Tests = tests;
            Count = count;
            Stopwatch = new Stopwatch();
        }

        private AbstractTests Tests { get; }
        private int Count { get; }
        private Action ArrangeCallback { get; set; }
        private int ActDelay { get; set; }
        private Func<AbstractAnimation> ActCallback { get; set; }
        private Func<IEnumerator> CustomWaiterCallback { get; set; }
        private int AssertDelay { get; set; }
        private Action AssertCallback { get; set; }
        private Action AbrogateCallback { get; set; }
        protected AbstractAnimation ControlAnimation { get; set; }
        protected Stopwatch Stopwatch { get; }
        private int CompletedTweens { get; set; }

        public AnimationTester Arrange(Action callback)
        {
            ArrangeCallback = callback;
            return this;
        }

        public AnimationTester SetActDelay(int actDelay)
        {
            ActDelay = actDelay;
            return this;
        }

        public AnimationTester Act(Func<AbstractAnimation> callback)
        {
            ActCallback = callback;
            return this;
        }

        public AnimationTester Act(Action callback)
        {
            ActCallback = () =>
            {
                callback();
                return null;
            };
            return this;
        }

        public AnimationTester SetCustomWaiter(Func<IEnumerator> callback)
        {
            CustomWaiterCallback = callback;
            return this;
        }

        public AnimationTester SetAssertDelay(int assertDelay)
        {
            AssertDelay = assertDelay;
            return this;
        }

        public AnimationTester AssertTime(Action<Stopwatch> callback)
        {
            AssertCallback += () => callback?.Invoke(Stopwatch);
            return this;
        }

        public AnimationTester AssertTime(float time)
        {
            AssertTime(() => time);
            return this;
        }

        public AnimationTester AssertTime(Func<float> getTime)
        {
            AssertTime((stopwatch) => FlowEntAssert.Time(getTime() + ControlAnimation.OverDraft.Value, (float)stopwatch.Elapsed.TotalSeconds));
            return this;
        }

        public AnimationTester Assert(Action callback)
        {
            AssertCallback = callback;
            return this;
        }

        public AnimationTester Assert<TAnimation>(Action<TAnimation> callback)
            where TAnimation : AbstractAnimation
        {
            AssertCallback = () => callback.Invoke((TAnimation)ControlAnimation);
            return this;
        }

        public AnimationTester Abrogate(Action callback)
        {
            AbrogateCallback = callback;
            return this;
        }

        public IEnumerator Run(string overtimeReason = "", float? maxRunTime = null)
        {
            float internalMaxRunTime;
            if (maxRunTime == null)
            {
                internalMaxRunTime = MaxRunTime;
            }
            else
            {
                if (string.IsNullOrEmpty(overtimeReason))
                {
                    throw new ArgumentException("You specified a different run time. You need to provide a reason for the overtime");
                }
                else
                {
                    internalMaxRunTime = maxRunTime.Value + OvertimeAllowance;
                }
            }

            Tests.CreateObjects(Count);
            yield return WaitForFrames(5);
            ArrangeCallback?.Invoke();
            if (ActDelay > 0)
            {
                yield return WaitForFrames(ActDelay);
            }
            ControlAnimation = ActCallback.Invoke();
            Stopwatch.Start();
            if (CustomWaiterCallback != null)
            {
                yield return CustomWaiterCallback.Invoke();
            }
            else
            {
                if (ControlAnimation != null)
                {
                    ControlAnimation.OnCompleted(OnComplete);
                    yield return WaitToCompleteAllTweens(Count);
                }
            }
            Stopwatch.Stop();
            if (Stopwatch.Elapsed.TotalSeconds > internalMaxRunTime)
            {
                throw new TimeoutException($"Test took too long. Expected: {internalMaxRunTime} but was {Stopwatch.Elapsed.TotalSeconds}.");
            }
            else
            {
                if (Stopwatch.Elapsed.TotalSeconds > MaxRunTime)
                {
                    Debug.LogWarning($"Test went to overtime. Reason: {overtimeReason}. Time: {Stopwatch.Elapsed.TotalSeconds}");
                }
            }
            if (ActDelay > 0)
            {
                yield return WaitForFrames(AssertDelay);
            }
            AssertCallback?.Invoke();
            AbrogateCallback?.Invoke();
        }

        private void OnComplete()
            => CompletedTweens++;

        private IEnumerator WaitToCompleteAllTweens(int count)
        {
            while (CompletedTweens < count)
            {
                yield return null;
            }
        }

        private IEnumerator WaitForFrames(int frames)
        {
            while (frames > 0)
            {
                frames--;
                yield return null;
            }
        }
    }
}