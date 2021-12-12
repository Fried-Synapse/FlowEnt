using UnityEngine;
using UnityEditor;

namespace FriedSynapse.Quickit.Editor
{
    [CustomEditor(typeof(Transform))]
    public sealed class TransformResetButtons : UnityEditor.Editor
    {
        private static class Icon
        {
            public static GUIContent Reset = EditorGUIUtility.IconContent("winbtn_win_close@2x", "Reset");
            public static GUIContent Approximate = EditorGUIUtility.IconContent("Linked", "Integer values");
        }
        private static class Style
        {
            public static GUIStyle Button = GetButtonStyle();
            public static float ButtonSize = EditorGUIUtility.singleLineHeight;
            public const float LabelWidth = 15f;

            private static GUIStyle GetButtonStyle()
            {
                GUIStyle style = new GUIStyle(GUI.skin.box);
                style.imagePosition = ImagePosition.ImageOnly;
                style.padding = new RectOffset(1, 1, 1, 1);
                style.border = new RectOffset(0, 0, 0, 0);
                return style;
            }
        }

        private Transform transform;

#pragma warning disable IDE0051, RCS1213
        private void OnEnable()
        {
            transform = target as Transform;
        }
#pragma warning restore IDE0051, RCS1213

        public override void OnInspectorGUI()
        {
            EditorGUIUtility.labelWidth = Style.LabelWidth;

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Position");
                GUILayout.FlexibleSpace();
                if (DrawButton(Icon.Reset, transform.localPosition != Vector3.zero))
                {
                    RecordUndo("Reset Position");
                    transform.localPosition = Vector3.zero;
                }
                if (DrawButton(Icon.Approximate))
                {
                    RecordUndo("Approximated Position");
                    transform.localPosition = Mathq.Round(transform.localPosition);
                }
                transform.localPosition = DrawVector3(transform.localPosition);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Rotation");
                GUILayout.FlexibleSpace();
                if (DrawButton(Icon.Reset, transform.localEulerAngles != Vector3.zero))
                {
                    RecordUndo("Reset Rotation");
                    transform.localEulerAngles = Vector3.zero;
                }
                if (DrawButton(Icon.Approximate))
                {
                    RecordUndo("Approximated Rotation");
                    transform.localEulerAngles = Mathq.Round(transform.localEulerAngles);
                }
                transform.localEulerAngles = DrawVector3(transform.localEulerAngles);
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Scale");
                GUILayout.FlexibleSpace();
                if (DrawButton(Icon.Reset, transform.localScale != Vector3.one))
                {
                    RecordUndo("Reset Scale");
                    transform.localScale = Vector3.one;
                }
                if (DrawButton(Icon.Approximate))
                {
                    RecordUndo("Approximated Scale");
                    transform.localScale = Mathq.Round(transform.localScale);
                }
                transform.localScale = DrawVector3(transform.localScale);
            }
            EditorGUILayout.EndHorizontal();
        }

        private bool DrawButton(GUIContent content, bool enabled = true)
        {
            GUI.enabled = enabled;
            bool result = GUILayout.Button(content, Style.Button, GUILayout.Width(Style.ButtonSize), GUILayout.Height(Style.ButtonSize));
            GUI.enabled = true;
            return enabled && result;
        }

        private Vector3 DrawVector3(Vector3 value)
        {
            GUILayoutOption width = GUILayout.MinWidth(30f);
            value.x = EditorGUILayout.FloatField("X", value.x, width);
            value.y = EditorGUILayout.FloatField("Y", value.y, width);
            value.z = EditorGUILayout.FloatField("Z", value.z, width);
            return value;
        }

        private void RecordUndo(string name)
        {
            Undo.RecordObject(transform, name);
            EditorUtility.SetDirty(transform);
        }
    }
}
