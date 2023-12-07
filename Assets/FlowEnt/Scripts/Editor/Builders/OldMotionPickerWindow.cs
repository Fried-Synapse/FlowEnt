using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class OldMotionPickerWindow : EditorWindow
    {
        private class TypeInfo
        {
            public Type Type { get; set; }
            public List<string> Names { get; set; } = new List<string>();
            public int SearchIndex { get; private set; }

            public void ComputeSearchIndex(List<string> parts)
            {
                SearchIndex = parts.Count(sp => Names.Any(n => n.IndexOf(sp, StringComparison.OrdinalIgnoreCase) >= 0));
            }
        }

        private static OldMotionPickerWindow instance;

        public static void Show<TMotionBuilder>(Action<TMotionBuilder> callback)
            where TMotionBuilder : IMotionBuilder
        {
            instance?.Close();
            instance = CreateWindow<OldMotionPickerWindow>("Select animation");
            instance.types = GetTypes<TMotionBuilder>().OrderBy(t => t.Names[0]).ToList();
            instance.callback = type => callback.Invoke((TMotionBuilder)Activator.CreateInstance(type));
            instance.Init();
            instance.ShowPopup();
        }

        private Action<Type> callback;
        private string searchText;
        private List<TypeInfo> types = new List<TypeInfo>();
        private Vector2 scrollPosition;
        private GUIStyle buttonStyle;
        private bool hadInitialFocus;

        private void Init()
        {
            buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.alignment = TextAnchor.MiddleLeft;
        }

#pragma warning disable IDE0051, RCS1213
        private void OnGUI()
        {
            GUI.SetNextControlName("SearchField");
            searchText = EditorGUILayout.TextField(searchText);
            if (!hadInitialFocus)
            {
                hadInitialFocus = true;
                EditorGUI.FocusTextInControl("SearchField");
            }

            List<TypeInfo> types = this.types;
            if (!string.IsNullOrEmpty(searchText))
            {
                List<string> searchParts = searchText
                    .Split(" ")
                    .Where(sp => !string.IsNullOrEmpty(sp))
                    .ToList();

                types.ForEach(t => t.ComputeSearchIndex(searchParts));
                types = types.Where(t => t.SearchIndex > 0).OrderByDescending(t => t.SearchIndex).ToList();
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, GUILayout.Width(position.width),
                GUILayout.Height(position.height));

            foreach (TypeInfo typeInfo in types)
            {
                if (GUILayout.Button(typeInfo.Names[0], buttonStyle))
                {
                    callback.Invoke(typeInfo.Type);
                    instance?.Close();
                }
            }

            EditorGUILayout.EndScrollView();
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
                objects.AddRange(assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(type))
                    .Select(t => new TypeInfo() { Type = t, Names = MotionNames.GetNames(t).All }));
            }

            return objects;
        }
    }
}