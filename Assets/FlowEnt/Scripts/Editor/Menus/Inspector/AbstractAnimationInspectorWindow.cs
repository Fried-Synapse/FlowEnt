using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
using FriedSynapse.FlowEnt.Reflection;
#endif

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractAnimationInspectorWindow : EditorWindow
    {
    }

    public abstract class AbstractAnimationInspectorWindow<TWindow, TAnimation> : AbstractAnimationInspectorWindow
        where TWindow : AbstractAnimationInspectorWindow<TWindow, TAnimation>
        where TAnimation : AbstractAnimation
    {
        private InspectorControllableSection controllableSection;
        private Vector2 scrollPosition;
        protected TAnimation Animation { get; private set; }
        private static Type[] types;

        public static void Show(TAnimation animation)
        {
            if (types == null)
            {
                Type type = typeof(AbstractAnimationInspectorWindow);
                types = type.Assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(type)).ToArray();
            }
            TWindow window = CreateWindow<TWindow>(animation.ToString(), types);
            window.Animation = animation;
            window.controllableSection = new InspectorControllableSection(animation);
            window.Show();
        }

#pragma warning disable IDE0051, RCS1213
        private void Update()
        {
            Repaint();
        }

        protected abstract void OnGuiInternal();

        private void OnGUI()
        {
            if (Animation == null)
            {
                Close();
                return;
            }

            FlowEntEditorGUILayout.LabelField(Animation, Animation.GetType().Name);
            EditorGUILayout.LabelField("Update Type:", Animation.GetPropertyValue<UpdateType>("UpdateType").ToString());

            controllableSection.Show();

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

            GUILayout.Space(10);

            ShowStackTrace();

            OnGuiInternal();

            EditorGUILayout.EndScrollView();

            GUILayout.Space(10);
            if (GUILayout.Button("Close"))
            {
                Close();
            }
            GUILayout.Space(10);
        }
#pragma warning restore IDE0051, RCS1213

        private void ShowStackTrace()
        {
            FlowEntEditorGUILayout.LabelFieldBold("Stack Trace:");
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
            string stackTrace = Animation.GetFieldValue<string>("stackTrace");
            if (!string.IsNullOrEmpty(stackTrace))
            {
                foreach (string line in stackTrace.Split('\n'))
                {
                    FlowEntEditorGUILayout.LabelField(line, FlowEntConstants.Orange, GUILayout.Width(position.width - 20f));
                }
            }
#else
            EditorGUILayout.HelpBox("Stack trace only available when debugging enabled. Please enable it in settings.", MessageType.Info);
            if (GUILayout.Button("Open settings"))
            {
                FlowEntMenu.ShowSettings();
            }
#endif
        }
    }
}
