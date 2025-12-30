using System.Linq;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public abstract class AbstractIfAttribute : PropertyAttribute
    {
        protected AbstractIfAttribute(string field, params object[] comparisonValues)
        {
            Field = field;
            ComparisonValues = comparisonValues;
        }

        protected AbstractIfAttribute(string field, object comparisonValue)
            : this(field, new[] { comparisonValue })
        {
        }

        public string Field { get; }
        public object[] ComparisonValues { get; }

        public bool HasValue(object value) => ComparisonValues.Contains(value);
    }
}