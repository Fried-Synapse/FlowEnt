using UnityEditor;
using UnityEngine;
using UnityEditorEditor = UnityEditor.Editor;

namespace FriedSynapse.Release

{
    [CustomEditor(typeof(ReleaseData))]
    public class ReleaseDataEditor : UnityEditorEditor
    {
        private ReleaseData ReleaseData { get; set; }
        private void OnEnable()
        {
            ReleaseData = (ReleaseData)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Data", EditorStyles.boldLabel);

            ShowDestination();
            ReleaseVersionField();
            EditorGUILayout.Space(10);
            ShowFilename();

            serializedObject.ApplyModifiedProperties();
        }

        private void ReleaseVersionField()
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("releaseVersion"), GUILayout.Width(EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth));
            string[] version = serializedObject.FindProperty("releaseVersion").stringValue.Substring(1).Split('.');
            int major = int.Parse(version[0]);
            int minor = int.Parse(version[1]);
            int patch = int.Parse(version[2]);

            GUILayout.BeginVertical(GUILayout.ExpandWidth(false));
            {
                static int getNumber(string name, int value)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(name, GUILayout.Width(35));
                    if (GUILayout.Button("^", GUILayout.MaxWidth(20)))
                    {
                        value++;
                    }

                    if (GUILayout.Button("˅", GUILayout.MaxWidth(20)))
                    {
                        value--;
                    }
                    if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
                    {
                        value = 0;
                    }

                    GUILayout.EndVertical();

                    return value;
                }

                major = getNumber("major", major);
                minor = getNumber("minor", minor);
                patch = getNumber("patch", patch);
            }

            GUILayout.EndVertical();

            serializedObject.FindProperty("releaseVersion").stringValue = $"v{major}.{minor}.{patch}";
            EditorGUILayout.EndHorizontal();
        }

        private void ShowDestination()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Destination", GUILayout.Width(EditorGUIUtility.labelWidth - 4));
                EditorGUILayout.LabelField(ReleaseData.Destination);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void ShowFilename()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Filename", GUILayout.Width(EditorGUIUtility.labelWidth));
                EditorGUILayout.LabelField(ReleaseData.GetFileName());
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}