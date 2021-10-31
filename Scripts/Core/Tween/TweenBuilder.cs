using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class TweenBuilder
    {
        [SerializeField]
        private TweenOptions options;
        public TweenOptions Options => options;
    }
}
