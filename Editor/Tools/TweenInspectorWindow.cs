using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class TweenInspectorWindow : EditorWindow
    {
        private Vector2 motionsScrollPosition;
        private Vector2 stackTraceScrollPosition;

        private Tween Tween { get; set; }
        public static void Show(Tween tween)
        {
            TweenInspectorWindow window = CreateWindow<TweenInspectorWindow>(tween.ToString());
            window.Tween = tween;
            window.ShowPopup();
        }

        private void OnGUI()
        {
            if (Tween == null)
            {
                Close();
                return;
            }

            FlowEntEditorGUILayout.LabelField(Tween, "Tween");

            GUILayout.Space(30);
            FlowEntEditorGUILayout.LabelFieldBold("Motions:");

            motionsScrollPosition = EditorGUILayout.BeginScrollView(motionsScrollPosition, GUILayout.Width(position.width - 20f));
            EditorGUI.indentLevel++;
            foreach (IMotion motion in Tween.GetFieldValue<IMotion[]>("motions"))
            {
                FlowEntEditorGUILayout.LabelField(motion);
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndScrollView();

            GUILayout.Space(30);

            FlowEntEditorGUILayout.LabelFieldBold("Stack Trace:");
#if FlowEnt_Debug
            stackTraceScrollPosition = EditorGUILayout.BeginScrollView(stackTraceScrollPosition, GUILayout.Width(200f));
            foreach (string line in Tween.GetFieldValue<string>("stackTrace").Split('\n'))
            {
                FlowEntEditorGUILayout.LabelField(line, FlowEntConstants.Orange);
            }
            EditorGUILayout.EndScrollView();
#else
            EditorGUILayout.HelpBox("Stack trace only available when debugging enabled. Please enable it in settings.", MessageType.Info);
            if (GUILayout.Button("Open settings"))
            {
                FlowEntSettingsWindow.Init();
            }
#endif

            GUILayout.Space(70);
            if (GUILayout.Button("Close"))
            {
                Close();
            }
            GUILayout.Space(20);
        }
    }
}
