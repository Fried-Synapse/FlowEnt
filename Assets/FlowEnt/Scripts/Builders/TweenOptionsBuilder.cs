using System;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenOptionsBuilder : AbstractBuilder<TweenOptions>
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public bool autoStart;
        [SerializeField]
        public int skipFrames;
        [SerializeField]
        public float delay;
        [SerializeField]
        private float timeScale = 1;
        [SerializeField]
        private float time = 1f;
        [SerializeField]
        private Easing easing = Easing.Linear;
        [SerializeField]
        private int loopCount = 1;
        [SerializeField]
        private LoopType loopType;
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
                LoopCount = loopCount,
                LoopType = loopType
            };
    }
}
