using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [AttributeUsage(AttributeTargets.Field)]
    public class UrlButtonAttribute : PropertyAttribute
    {
        public enum PredefinedType
        {
            Easing,
            Curves
        }
        
        public UrlButtonAttribute(string url, string tooltip)
        {
            Url = url;
            Tooltip = tooltip;
        }
        
        
        public UrlButtonAttribute(PredefinedType type)
        {
            switch (type)
            {
                case PredefinedType.Easing:
                    Url = "https://easings.net/";
                    Tooltip = "easings.net";
                    break;
                case PredefinedType.Curves:
                    Url = "https://docs.flowent.friedsynapse.com/manual/concepts/curves.html";
                    Tooltip = "Curves";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            
        }
        
        public string Url { get; }
        public string Tooltip { get; }
    }
}