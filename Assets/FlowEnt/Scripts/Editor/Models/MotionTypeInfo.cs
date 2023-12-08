using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class MotionTypeInfo
    {
        internal MotionTypeInfo(Type type)
        {
            Type = type;
            Names = MotionNames.GetNames(type);
        }

        internal Type Type { get; }
        internal MotionNames Names { get; }
        internal int SearchIndex { get; private set; }

        internal void ComputeSearchIndex(List<string> parts)
        {
            SearchIndex = parts.Count(sp => Names.All.Any(n => n.IndexOf(sp, StringComparison.OrdinalIgnoreCase) >= 0));
        }

        internal string GetTooltip()
        {
            const string flowEntAssembly = "FriedSynapse.FlowEnt";

            string result = string.Empty;
            TooltipAttribute tooltipAttribute = Type.GetCustomAttribute<TooltipAttribute>();
            if (tooltipAttribute != null)
            {
                result += $"{tooltipAttribute.tooltip}\n";
            }
            
            if (Type.Assembly.GetName().Name == flowEntAssembly)
            {
                if (Names.Preferred.EndsWith(" Value"))
                {
                    result += "*Value* - It will interpolate starting at the [Current State] and end at [Current State] + [Value].\n";
                }
                
                if (Names.Preferred.EndsWith(" From To"))
                {
                    result += "*From To* - It will interpolate starting at the [From] and ending at [To]. (Not available in the editor yet) - If [From] is not specified it will use it's [Current State].\n";
                }
            }
            
            return result.Trim();
        }

        internal static List<MotionTypeInfo> GetTypes<TMotionBuilder>()
            where TMotionBuilder : IMotionBuilder
        {
            List<MotionTypeInfo> objects = new List<MotionTypeInfo>();
            Type type = typeof(TMotionBuilder);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                objects.AddRange(assembly
                    .GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(type))
                    .Select(t => new MotionTypeInfo(t)));
            }

            return objects;
        }
    }
}