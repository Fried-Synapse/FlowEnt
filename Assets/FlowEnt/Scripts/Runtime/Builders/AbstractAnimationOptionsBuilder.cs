using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public abstract class AbstractAnimationOptionsBuilder<TAnimationOptions> : AbstractBuilder<TAnimationOptions>
        where TAnimationOptions : AbstractAnimationOptions, new()
    {
#pragma warning disable RCS1169, RCS1085, IDE0044
        [SerializeField]
        private string name;
        public string Name => name;
        [SerializeField]
        private UpdateType updateType;
        public UpdateType UpdateType => updateType;
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
        private int loopCount = 1;
        public int LoopCount => loopCount;
        [SerializeField]
        private bool isLoopCountInfinite;
        public bool IsLoopCountInfinite => isLoopCountInfinite;
#pragma warning restore RCS1169, RCS1085, IDE0044

        public override TAnimationOptions Build()
            => new TAnimationOptions()
            {
                Name = name,
                UpdateType = updateType,
                AutoStart = autoStart,
                SkipFrames = skipFrames,
                Delay = delay,
                TimeScale = timeScale,
                LoopCount = isLoopCountInfinite ? default(int?) : loopCount,
            };
    }
}
