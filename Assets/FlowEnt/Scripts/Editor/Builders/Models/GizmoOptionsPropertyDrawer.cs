using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(GizmoOptions), true)]
    public class GizmoOptionsPropertyDrawer : PropertyDrawer
    {
        private enum FieldsEnum
        {
            show,
            color,
            width,
        }

        private const string IsVisibleName = "isVisible";

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => property.FindPropertyRelative(IsVisibleName).boolValue
                ? FlowEntConstants.SpacedSingleLineHeight
                  + Enum.GetValues(typeof(FieldsEnum)).Cast<FieldsEnum>()
                      .Sum(f => EditorGUI.GetPropertyHeight(property.FindPropertyRelative(f.ToString())) + FlowEntConstants.DrawerSpacing)
                : 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!property.FindPropertyRelative(IsVisibleName).boolValue)
            {
                return;
            }

            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.LabelField(position, "Gizmo Options");
            position.y += FlowEntConstants.SpacedSingleLineHeight;
            EditorGUI.indentLevel++;
            foreach (FieldsEnum field in Enum.GetValues(typeof(FieldsEnum)).Cast<FieldsEnum>())
            {
                FlowEntEditorGUILayout.PropertyField(ref position, property.FindPropertyRelative(field.ToString()));
            }

            EditorGUI.indentLevel--;
        }
    }
}