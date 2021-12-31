using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class EchoOptionsBuilder : AbstractAnimationOptionsBuilder<EchoOptions>
    {
#pragma warning disable RCS1169, RCS1085, IDE0044
        [SerializeField]
        private float timeout;
        public float Timeout => timeout;
        [SerializeField]
        private bool hasTimeout;
        public bool HasTimeout => hasTimeout;
#pragma warning restore RCS1169, RCS1085, IDE0044

        public override EchoOptions Build()
        {
            EchoOptions options = base.Build();
            options.Timeout = hasTimeout ? timeout : default(float?);
            return options;
        }
    }
}
