using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractAnimationInspectorWindow<TWindow, TAnimation> : EditorWindow
        where TWindow : AbstractAnimationInspectorWindow<TWindow, TAnimation>
        where TAnimation : AbstractAnimation
    {
        private ControllableSection<TAnimation> controllableSection;
        private Vector2 scrollPosition;
        protected TAnimation Animation { get; set; }

        public static void Show(TAnimation animation)
        {
            TWindow window = CreateWindow<TWindow>(animation.ToString());
            window.Animation = animation;
            window.Show();
        }

#pragma warning disable IDE0051, RCS1213
        private void Update()
        {
            Repaint();
            controllableSection.Update();
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

            controllableSection ??= new ControllableSection<TAnimation>(Animation);
            controllableSection.ShowControls();

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
            foreach (string line in Animation.GetFieldValue<string>("stackTrace").Split('\n'))
            {
                FlowEntEditorGUILayout.LabelField(line, FlowEntConstants.Orange, GUILayout.Width(position.width - 20f));
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
