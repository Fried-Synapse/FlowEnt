using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EnableIfAttribute : PropertyAttribute
    {
        public EnableIfAttribute(string field, bool isInverted = false)
        {
            Field = field;
            IsInverted = isInverted;
        }
    
        public string Field { get; }
        public bool IsInverted { get; }
    }
}