using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class IdentifiableBuilderFields
    {
        internal const string DisplayName = "displayName";
        internal const string IsDisplayNameEnabled = "isDisplayNameEnabled";
        internal const string IsEnabled = "isEnabled";

        internal static float GetDisplayNameHeight(SerializedProperty property)
            => property.FindPropertyRelative(IsDisplayNameEnabled).boolValue
                ? FlowEntConstants.SpacedSingleLineHeight
                : 0;

        internal static void DrawDisplayName(ref Rect position, SerializedProperty property)
        {
            if (property.FindPropertyRelative(IsDisplayNameEnabled).boolValue)
            {
                FlowEntEditorGUILayout.PropertyFieldSingleLine(ref position,
                    property.FindPropertyRelative(DisplayName));
            }
        }

        internal static void DrawShowRename(SerializedProperty property, GenericMenu context)
        {
            SerializedProperty isNameEnabledProperty = property.FindPropertyRelative(IsDisplayNameEnabled);

            void showRename()
            {
                isNameEnabledProperty.boolValue = !isNameEnabledProperty.boolValue;
                isNameEnabledProperty.serializedObject.ApplyModifiedProperties();
            }

            context.AddItem(new GUIContent("Show Rename"), showRename, false, isNameEnabledProperty.boolValue);
        }
    }
}