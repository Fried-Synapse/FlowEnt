using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class Phase4Animation : AbstractDemoAnimation
    {
        [SerializeField]
        private List<Transform> penduls;
        private List<Transform> Penduls => penduls;

        public override AbstractAnimation GetAnimation()
            => new Flow()
                .Queue(new Tween());


    }
}
