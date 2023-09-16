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
        [SerializeField, Min(0)]
        private int skipFrames;
        public int SkipFrames => skipFrames;
        [SerializeField, Min(0f)]
        private float delay;
        public float Delay => delay;
        [SerializeField, Min(0f)]
        private float timeScale = AbstractAnimationOptions.DefaultTimeScale;
        public float TimeScale => timeScale;
        [SerializeField]
        private bool isLoopCountInfinite;
        [SerializeField, Min(1)]
        private int loopCount = AbstractAnimationOptions.DefaultLoopCount;
        public int? LoopCount => isLoopCountInfinite ? default(int?) : loopCount;
#pragma warning restore RCS1169, RCS1085, IDE0044

        public override TAnimationOptions Build()
            => new TAnimationOptions()
            {
                Name = Name,
                UpdateType = UpdateType,
                AutoStart = AutoStart,
                SkipFrames = SkipFrames,
                Delay = Delay,
                TimeScale = TimeScale,
                LoopCount = LoopCount
            };
    }
}
