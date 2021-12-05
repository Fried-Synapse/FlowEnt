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
        public string Name { get => name; set => name = value; }
        [SerializeField]
        private bool autoStart;
        public bool AutoStart { get => autoStart; set => autoStart = value; }
        [SerializeField]
        private int skipFrames;
        public int SkipFrames { get => skipFrames; set => skipFrames = value; }
        [SerializeField]
        private float delay;
        public float Delay { get => delay; set => delay = value; }
        [SerializeField]
        private float timeScale = AbstractAnimationOptions.DefaultTimeScale;
        public float TimeScale { get => timeScale; set => timeScale = value; }
        [SerializeField]
        private float time = TweenOptions.DefaultTime;
        public float Time { get => time; set => time = value; }
        [SerializeField]
        private Easing easing = TweenOptions.DefaultEasing;
        public Easing Easing { get => easing; set => easing = value; }
        [SerializeField]
        private int loopCount = 1;
        public int LoopCount { get => loopCount; set => loopCount = value; }
        [SerializeField]
        private bool isLoopCountInfinite;
        public bool IsLoopCountInfinite { get => isLoopCountInfinite; set => isLoopCountInfinite = value; }
        [SerializeField]
        private LoopType loopType;
        public LoopType LoopType { get => loopType; set => loopType = value; }

#pragma warning restore RCS1169, RCS1085, IDE0044

        public override TweenOptions Build()
            => new TweenOptions()
            {
                Name = Name,
                AutoStart = AutoStart,
                SkipFrames = SkipFrames,
                Delay = Delay,
                TimeScale = TimeScale,
                Time = Time,
                Easing = EasingFactory.Create(Easing),
                LoopCount = IsLoopCountInfinite ? default(int?) : LoopCount,
                LoopType = LoopType
            };
    }
}
