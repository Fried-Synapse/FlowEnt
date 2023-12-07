using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class MotionNames
    {
        private static readonly Regex nestedRegEx = new Regex("\\+");
        private static readonly Regex prettyNameRegEx = new Regex("[A-Z]");

        internal string DisplayName { get; private set; }
        internal string AttributeDisplayName { get; private set; }
        internal string PrettyName { get; private set; }
        internal string Name { get; private set; }
        internal string FullName { get; private set; }

        private string preferred;

        internal string Preferred =>
            preferred ??= DisplayName ?? AttributeDisplayName ?? PrettyName ?? Name ?? FullName;

        private List<string> all;

        internal List<string> All
        {
            get
            {
                if (all == null)
                {
                    all = new List<string>();
                    tryAdd(DisplayName);
                    tryAdd(AttributeDisplayName);
                    tryAdd(PrettyName);
                    tryAdd(Name);
                    tryAdd(FullName);
                }

                return all;

                void tryAdd(string name)
                {
                    if (!string.IsNullOrEmpty(name))
                    {
                        all.Add(name);
                    }
                }
            }
        }

        internal static MotionNames GetNames(Type type, IMotionBuilder motionBuilder = null)
        {
            MotionNames names = new MotionNames();
            if (motionBuilder != null && !string.IsNullOrEmpty(motionBuilder.DisplayName))
            {
                names.DisplayName = motionBuilder.DisplayName;
            }

            DisplayNameAttribute displayNameAttribute = type.GetCustomAttribute<DisplayNameAttribute>();
            if (displayNameAttribute != null)
            {
                names.AttributeDisplayName = displayNameAttribute.DisplayName;
            }

            names.PrettyName = getPrettyName(type);
            names.Name = type.Name;
            names.FullName = type.FullName;
            return names;

            static string getPrettyName(Type type)
            {
                const string builder = "Builder";
                const string flowEntAssembly = "FriedSynapse.FlowEnt";

                string[] nameParts = type.FullName.Split('.');
                string prettyName = nameParts[nameParts.Length - 1];
                prettyName = nestedRegEx.Replace(prettyName, " -");
                prettyName = prettyNameRegEx.Replace(prettyName, " $0");
                string[] prettyParts = prettyName.Split(' ');
                if (prettyParts[prettyParts.Length - 1] == builder)
                {
                    prettyName = Regex.Replace(prettyName, $"{(type.Name == builder ? $" - {builder}" : builder)}$",
                        string.Empty);
                }

                if (type.Assembly.GetName().Name == flowEntAssembly)
                {
                    prettyName = $"[{type.Namespace.Split('.').Last()}]{prettyName}";
                }

                return prettyName.TrimStart();
            }
        }
    }
}