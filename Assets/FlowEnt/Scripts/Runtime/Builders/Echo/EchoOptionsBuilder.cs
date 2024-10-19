using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class EchoOptionsBuilder : AbstractAnimationOptionsBuilder<EchoOptions>
    {
        [SerializeField]
        private bool hasTimeout;

        [SerializeField, Min(EchoOptions.MinTime)]
        private float timeout = EchoOptions.DefaultTime;

        public float? Timeout => hasTimeout ? timeout : default(float?);

        public override EchoOptions Build()
        {
            EchoOptions options = base.Build();
            options.Timeout = Timeout;
            return options;
        }
    }
}