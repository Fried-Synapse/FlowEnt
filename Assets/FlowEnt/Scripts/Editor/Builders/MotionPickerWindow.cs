using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class MotionPickerWindow : EditorWindow
    {
        private class TypeInfo
        {
            public Type Type { get; set; }
            public List<string> Names { get; set; } = new List<string>();
        }

        private static MotionPickerWindow instance;
        public static void Show<TMotionBuilder>(Action<TMotionBuilder> callback)
            where TMotionBuilder : IMotionBuilder
        {
            instance?.Close();
            instance = CreateWindow<MotionPickerWindow>("Select animation");
            instance.types = GetTypes<TMotionBuilder>();
            instance.callback = type => callback.Invoke((TMotionBuilder)Activator.CreateInstance(type));
            instance.ShowPopup();
        }

        private Action<Type> callback;
        private string searchText;
        private List<TypeInfo> types = new List<TypeInfo>();

#pragma warning disable IDE0051, RCS1213
        private void OnGUI()
        {
            searchText = EditorGUILayout.TextField(searchText);

            List<TypeInfo> types = this.types;
            if (!string.IsNullOrEmpty(searchText))
            {
                types = types.FindAll(t => t.Names.Any(n => n.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            foreach (TypeInfo typeInfo in types)
            {
                if (GUILayout.Button(typeInfo.Names[0]))
                {
                    callback.Invoke(typeInfo.Type);
                    instance?.Close();
                }
            }
        }

        private void OnLostFocus()
        {
            EditorApplication.delayCall += () => instance?.Close();
        }
#pragma warning restore IDE0051, RCS1213

        private static List<TypeInfo> GetTypes<TMotionBuilder>()
            where TMotionBuilder : IMotionBuilder
        {
            List<TypeInfo> objects = new List<TypeInfo>();
            Type type = typeof(TMotionBuilder);

            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                objects.AddRange(assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(type)).Select(t => new TypeInfo() { Type = t, Names = AbstractMotionBuilderPropertyDrawer.GetNames(t) }));
            }
            return objects;
        }
    }
}
