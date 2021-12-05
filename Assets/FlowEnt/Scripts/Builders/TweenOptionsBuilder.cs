using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenOptionsBuilder : AbstractBuilder<TweenOptions>
    {
#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private string name;
        [SerializeField]
        private bool autoStart;
        [SerializeField]
        private int skipFrames;
        [SerializeField]
        private float delay;
        [SerializeField]
        private float timeScale = 1;
        [SerializeField]
        private float time = 1f;
        [SerializeField]
        private Easing easing = Easing.Linear;
        [SerializeField]
        private int loopCount = 1;
        [SerializeField]
        private bool isLoopCountInfinite;
        [SerializeField]
        private LoopType loopType;
#pragma warning restore RCS1169, IDE0044

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
