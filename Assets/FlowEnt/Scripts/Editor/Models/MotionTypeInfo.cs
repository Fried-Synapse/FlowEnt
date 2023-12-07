using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        internal string GetToolTip()
        {
            return "";
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