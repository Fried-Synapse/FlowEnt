using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class IdentifiableBuilderFields
    {
        public const string DisplayName = "displayName";
        public const string IsDisplayNameEnabled = "isDisplayNameEnabled";
        public const string IsEnabled = "isEnabled";

        public static float GetDisplayNameHeight(SerializedProperty property)
            => property.FindPropertyRelative(IsDisplayNameEnabled).boolValue
                ? FlowEntConstants.SpacedSingleLineHeight
                : 0;

        public static void DrawDisplayName(ref Rect position, SerializedProperty property)
        {
            if (property.FindPropertyRelative(IsDisplayNameEnabled).boolValue)
            {
                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative(DisplayName));
                position.y += FlowEntConstants.SpacedSingleLineHeight;
            }
        }

        public static void DrawShowRename(SerializedProperty property, GenericMenu context)
        {
            SerializedProperty isNameEnabledProperty = property.FindPropertyRelative(IsDisplayNameEnabled);

            void showRename()
            {
                isNameEnabledProperty.boolValue = !isNameEnabledProperty.boolValue;
                isNameEnabledProperty.serializedObject.ApplyModifiedProperties();
            }

            context.AddItem(new GUIContent("Show Rename"), showRename, false, isNameEnabledProperty.boolValue);
            context.ShowAsContext();
        }
    }
}