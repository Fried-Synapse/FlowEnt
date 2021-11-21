using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Easings;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class Phase5Animation : AbstractDemoAnimation
    {
        [SerializeField]
        private List<Light> lights;
        private List<Light> Lights => lights;

        [SerializeField]
        private AnimationCurve flicker;
        private AnimationCurve Flicker => flicker;

        public override AbstractAnimation GetAnimation()
        {
            return new Tween(1f).SetEasing(new AnimationCurveEasing(Flicker)).ForAll(Lights).Apply(l => l.IntensityTo(l.Item.gameObject.name == "Centre" ? 5f : 8f));
        }
    }
}