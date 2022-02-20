using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(IMotionBuilder), true)]
    public class AbstractMotionBuilderPropertyDrawer : PropertyDrawer
    {
        private readonly List<string> hiddenProperties = new List<string>{
            "name",
            "isEnabled",
        };

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight;
            ForEachVisibleProperty(property, p => height += EditorGUI.GetPropertyHeight(p, true));
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect headerPosition = FlowEntEditorGUILayout.GetRect(position, 0);
            SerializedProperty nameProperty = property.FindPropertyRelative("name");
            label.text = $"        {(!string.IsNullOrEmpty(nameProperty.stringValue) ? nameProperty.stringValue : property.GetValue<IMotionBuilder>().GetType().Name)}";
            property.isExpanded = EditorGUI.Foldout(headerPosition, property.isExpanded, label);

            Rect isEnabledPosition = headerPosition;
            isEnabledPosition.x = EditorGUIUtility.singleLineHeight * 1.2f;
            isEnabledPosition.width = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(isEnabledPosition, property.FindPropertyRelative("isEnabled"), GUIContent.none);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            position.y += FlowEntConstants.SpacedSingleLineHeight;
            ForEachVisibleProperty(property, p =>
            {
                float height = EditorGUI.GetPropertyHeight(p, true) + FlowEntConstants.DrawerSpacing;
                position.height = height;
                EditorGUI.PropertyField(position, p, true);
                position.y += height;
            });
            EditorGUI.indentLevel--;
        }

        private void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
        {
            FlowEntEditorGUILayout.ForEachVisibleProperty(property, p =>
            {
                if (!hiddenProperties.Contains(p.name))
                {
                    predicate(p);
                }
            });
        }
    }
}
