using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnableIfAttribute : AbstractIfAttribute
    {
        public EnableIfAttribute(string field, params object[] comparisonValues)
            : base(field, comparisonValues)
        {
        }

        public EnableIfAttribute(string field, object comparisonValue)
            : base(field, comparisonValue)
        {
        }
    }
}