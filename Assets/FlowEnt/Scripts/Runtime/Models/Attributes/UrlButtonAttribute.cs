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
            Spline
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
                case PredefinedType.Spline:
                    Url = "https://flowent.friedsynapse.com/splines";
                    Tooltip = "Splines";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
            
        }
        
        public string Url { get; }
        public string Tooltip { get; }
    }
}