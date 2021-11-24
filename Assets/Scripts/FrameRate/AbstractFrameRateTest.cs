using System;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    public abstract class AbstractFrameRateTest
    {
        protected AbstractFrameRateTest(FrameRateTestController controller, float testTime, int testAmount)
        {
            Controller = controller;
            internalTestTime = testTime;
            TestTime = testTime;
            //one for control
            internalTestAmount = testAmount - 1;
            TestAmount = testAmount;
        }

        protected readonly float internalTestTime;
        public float TestTime { get; }
        protected readonly int internalTestAmount;
        public int TestAmount { get; }
        protected FrameRateTestController Controller { get; }

        public double WarmupTime { get; private set; }
        public double FrameRate { get; private set; }
        public abstract string TestName { get; }

        protected abstract void Prepare();

        protected abstract Task StartControlAsync();

        public virtual void Load()
        {
        }

        public virtual void Unload()
        {
        }

        public virtual async Task RunAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Stopwatch warmupStopwatch = new Stopwatch();
            warmupStopwatch.Start();

            Prepare();

            warmupStopwatch.Stop();

            int framesBefore = Time.frameCount;
            Stopwatch frameRateStopwatch = new Stopwatch();
            frameRateStopwatch.Start();

            await StartControlAsync();

            int framesAfter = Controller.FramesCount;
            frameRateStopwatch.Stop();
            stopwatch.Stop();

            WarmupTime = warmupStopwatch.Elapsed.TotalMilliseconds;
            FrameRate = (framesAfter - framesBefore) / frameRateStopwatch.Elapsed.TotalSeconds;
        }
    }
}