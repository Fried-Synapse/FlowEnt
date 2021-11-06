using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class Phase3Animation : AbstractDemoAnimation
    {
        [SerializeField]
        private List<Transform> blowers;
        private List<Transform> Blowers => blowers;

        public override AbstractAnimation GetAnimation()
        {
            return new Flow()
                .Queue(new Tween(1f));
        }
    }
}
