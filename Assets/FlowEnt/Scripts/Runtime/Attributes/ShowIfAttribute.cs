using System;
using System.Linq;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ShowIfAttribute : AbstractIfAttribute
    {
        public ShowIfAttribute(string field, params object[] comparisonValues)
            : base(field, comparisonValues)
        {
        }

        public ShowIfAttribute(string field, object comparisonValue)
            : base(field, comparisonValue)
        {
        }
    }
}