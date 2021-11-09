using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractAnimationInspectorWindow<TWindow, TAnimation> : EditorWindow
        where TWindow : AbstractAnimationInspectorWindow<TWindow, TAnimation>
        where TAnimation : AbstractAnimation
    {
        private Vector2 scrollPosition;

        protected TAnimation Animation { get; set; }
        public static void Show(TAnimation animation)
        {
            TWindow window = CreateWindow<TWindow>(animation.ToString());
            window.Animation = animation;
            window.Show();
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


        private void ShowStackTrace()
        {
            FlowEntEditorGUILayout.LabelFieldBold("Stack Trace:");
#if FlowEnt_Debug
            foreach (string line in Animation.GetFieldValue<string>("stackTrace").Split('\n'))
            {
                FlowEntEditorGUILayout.LabelField(line, FlowEntConstants.Orange, GUILayout.Width(position.width - 20f));
            }
#else
            EditorGUILayout.HelpBox("Stack trace only available when debugging enabled. Please enable it in settings.", MessageType.Info);
            if (GUILayout.Button("Open settings"))
            {
                FlowEntSettingsWindow.Init();
            }
#endif
        }
    }
}
