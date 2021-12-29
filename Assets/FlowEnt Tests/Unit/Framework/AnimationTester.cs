using System;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class AnimationTester
    {
        public AnimationTester(AbstractTests tests, int count)
        {
            Tests = tests;
            Count = count;
            Stopwatch = new Stopwatch();
        }

        private AbstractTests Tests { get; }
        private int Count { get; }
        private Action ArrangeCallback { get; set; }
        private Func<AbstractAnimation> ActCallback { get; set; }
        private Func<IEnumerator> CustomWaiterCallback { get; set; }
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

        public AnimationTester Abrogate(Action callback)
        {
            AbrogateCallback = callback;
            return this;
        }

        public IEnumerator Run()
        {
            Tests.CreateObjects(Count);
            yield return WaitForFrames(5);
            ArrangeCallback?.Invoke();
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
            if (Stopwatch.Elapsed.TotalSeconds > 0.6)
            {
                Debug.LogWarning($"Very long test. Time spent: {Stopwatch.Elapsed.TotalSeconds}s");
            }
            yield return WaitForFrames(5);
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