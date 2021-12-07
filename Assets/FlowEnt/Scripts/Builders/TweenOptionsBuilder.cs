using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenOptionsBuilder : AbstractBuilder<TweenOptions>
    {
#pragma warning disable RCS1169, RCS1085, IDE0044
        [SerializeField]
        private string name;
        public string Name => name;
        [SerializeField]
        private bool autoStart;
        public bool AutoStart => autoStart;
        [SerializeField]
        private int skipFrames;
        public int SkipFrames => skipFrames;
        [SerializeField]
        private float delay;
        public float Delay => delay;
        [SerializeField]
        private float timeScale = AbstractAnimationOptions.DefaultTimeScale;
        public float TimeScale => timeScale;
        [SerializeField]
        private float time = TweenOptions.DefaultTime;
        public float Time => time;
        [SerializeField]
        private Easing easing = TweenOptions.DefaultEasing;
        public Easing Easing => easing;
        [SerializeField]
        private int loopCount = 1;
        public int LoopCount => loopCount;
        [SerializeField]
        private bool isLoopCountInfinite;
        public bool IsLoopCountInfinite => isLoopCountInfinite;
        [SerializeField]
        private LoopType loopType;
        public LoopType LoopType => loopType;

#pragma warning restore RCS1169, RCS1085, IDE0044

        public override TweenOptions Build()
            => new TweenOptions()
            {
                Name = name,
                AutoStart = autoStart,
                SkipFrames = skipFrames,
                Delay = delay,
                TimeScale = timeScale,
                Time = time,
                Easing = EasingFactory.Create(easing),
                LoopCount = isLoopCountInfinite ? default(int?) : loopCount,
                LoopType = loopType
            };
    }
}
