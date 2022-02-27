using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class MotionPickerWindow : EditorWindow
    {
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
        private List<Type> types = new List<Type>();

#pragma warning disable IDE0051, RCS1213
        private void OnGUI()
        {
            searchText = EditorGUILayout.TextField(searchText);

            List<Type> types = this.types;
            if (!string.IsNullOrEmpty(searchText))
            {
                types = types.FindAll(t => t.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            foreach (Type type in types)
            {
                if (GUILayout.Button(type.Name))
                {
                    callback.Invoke(type);
                    instance?.Close();
                }
            }
        }

        private void OnLostFocus()
        {
            EditorApplication.delayCall += () => instance?.Close();
        }
#pragma warning restore IDE0051, RCS1213

        public static List<Type> GetTypes<TMotionBuilder>()
            where TMotionBuilder : IMotionBuilder
        {
            List<Type> objects = new List<Type>();
            Type type = typeof(TMotionBuilder);
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                objects.AddRange(assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(type)));
            }
            return objects;
        }
    }
}
