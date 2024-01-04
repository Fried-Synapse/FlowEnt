using System;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class FlowEntEditorGUILayout
    {
        internal static void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
        {
            SerializedProperty copy = property.Copy();
            int baseDepth = copy.depth;
            copy.NextVisible(true);
            do
            {
                if (copy.depth <= baseDepth)
                {
                    break;
                }

                predicate(copy);
            } while (copy.NextVisible(false));
        }

        internal static Rect GetRect(Rect position, int index)
            => GetRect(position, index, FlowEntConstants.SpacedSingleLineHeight, EditorGUIUtility.singleLineHeight);

        internal static Rect GetRect(Rect position, int index, float spacedLineHeight, float lineHeight) =>
            new(position.x, position.y + (index * spacedLineHeight), position.width, lineHeight);

        internal static void PropertyField(ref Rect position, SerializedProperty property, GUIContent label = null)
        {
            position.height = EditorGUI.GetPropertyHeight(property);
            if (label == null)
            {
                EditorGUI.PropertyField(position, property);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }

            position.y += position.height + FlowEntConstants.DrawerSpacing;
        }

        internal static void PropertyFieldIsEnabled(Rect position, SerializedProperty property)
        {
            Rect isEnabledPosition = position;
            isEnabledPosition.x += 2f;
            isEnabledPosition.width = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(isEnabledPosition, property.FindPropertyRelative("isEnabled"), GUIContent.none);
        }
    }
}