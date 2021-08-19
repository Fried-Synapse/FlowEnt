using System;

namespace FriedSynapse.FlowEnt
{
    internal interface IFluentFlowOptionable<T>
    {
        T SetLoopCount(int? loopCount);

        T SetTimeScale(float timeScale);

        T SetSkipFrames(int frames);

        T SetDelay(float time);
    }

    public class FlowOptions : AbstractAnimationOptions, IFluentFlowOptionable<FlowOptions>
    {
        public FlowOptions(bool autoStart = false) : base(autoStart)
        {
        }

        #region Options

        public FlowOptions SetAutoStart(bool autoStart)
        {
            AutoStart = autoStart;
            return this;
        }

        public FlowOptions SetSkipFrames(int frames)
        {
            SkipFrames = frames;
            return this;
        }

        public FlowOptions SetDelay(float time)
        {
            Delay = time;
            return this;
        }

        public FlowOptions SetLoopCount(int? loopCount)
        {
            LoopCount = loopCount;
            return this;
        }

        public FlowOptions SetTimeScale(float timeScale)
        {
            TimeScale = timeScale;
            return this;
        }

        #endregion

    }
}
