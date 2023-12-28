using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class EchoOptionsBuilder : AbstractAnimationOptionsBuilder<EchoOptions>
    {
#pragma warning disable RCS1169, RCS1085, IDE0044
        [SerializeField]
        private bool hasTimeout;

        [SerializeField, Min(EchoOptions.MinTime)]
        private float timeout = EchoOptions.DefaultTime;

        public float? Timeout => hasTimeout ? timeout : default(float?);
#pragma warning restore RCS1169, RCS1085, IDE0044

        public override EchoOptions Build()
        {
            EchoOptions options = base.Build();
            options.Timeout = Timeout;
            return options;
        }
    }
}