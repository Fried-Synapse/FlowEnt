using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(GizmoOptions), true)]
    public class GizmoOptionsPropertyDrawer : PropertyDrawer
    {
        private const float Padding = 3;

        private enum FieldsEnum
        {
            isVisible,
            show,
            color,
            width,
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => property.FindPropertyRelative(FieldsEnum.isVisible.ToString()).boolValue
                ? FlowEntConstants.SpacedSingleLineHeight
                  + Enum.GetValues(typeof(FieldsEnum)).Cast<FieldsEnum>()
                      .Sum(f => EditorGUI.GetPropertyHeight(property.FindPropertyRelative(f.ToString())) + FlowEntConstants.DrawerSpacing)
                  + Padding
                : 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!property.FindPropertyRelative(FieldsEnum.isVisible.ToString()).boolValue)
            {
                return;
            }

            DrawBox(position);

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, label);
            position.y += FlowEntConstants.SpacedSingleLineHeight;

            EditorGUI.indentLevel++;
            foreach (FieldsEnum field in Enum.GetValues(typeof(FieldsEnum)).Cast<FieldsEnum>().Where(f => f != FieldsEnum.isVisible))
            {
                FlowEntEditorGUILayout.PropertyField(ref position, property.FindPropertyRelative(field.ToString()));
            }

            EditorGUI.indentLevel--;
        }

        private static void DrawBox(Rect position)
        {
            position.height -= Padding;
            position.x -= Padding;
            position.width += Padding * 2;
            GUI.Box(position, GUIContent.none, EditorStyles.helpBox);
        }
    }
}