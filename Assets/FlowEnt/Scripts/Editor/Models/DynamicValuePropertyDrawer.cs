using System;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(DynamicValue<>), true)]
    public class DynamicValuePropertyDrawer : PropertyDrawer
    {
        private enum PropertiesEnum
        {
            type,
            constant,
            randomMin,
            randomMax,
        }

        const float MenuWidth = 20f;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => (DynamicValueType)property.FindPropertyRelative(PropertiesEnum.type.ToString()).enumValueIndex switch
            {
                DynamicValueType.Random =>
                    EditorGUI.GetPropertyHeight(property.FindPropertyRelative(PropertiesEnum.randomMin.ToString())) +
                    EditorGUI.GetPropertyHeight(property.FindPropertyRelative(PropertiesEnum.randomMax.ToString())) +
                    FlowEntConstants.DrawerSpacing,
                _ => EditorGUI.GetPropertyHeight(property.FindPropertyRelative(PropertiesEnum.constant.ToString())),
            };

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DrawDropdown(position, property);
            position.height = EditorGUIUtility.singleLineHeight;
            position.width -= MenuWidth + FlowEntConstants.DrawerSpacing;
            switch ((DynamicValueType)property.FindPropertyRelative(PropertiesEnum.type.ToString()).enumValueIndex)
            {
                case DynamicValueType.Constant:
                    EditorGUI.PropertyField(position,
                        property.FindPropertyRelative(PropertiesEnum.constant.ToString()), label);
                    break;
                case DynamicValueType.Random:
                    Rect labelPosition = position;
                    labelPosition.width = EditorGUIUtility.labelWidth - 13;
                    EditorGUI.LabelField(labelPosition, label);
                    position.width -= labelPosition.width;
                    position.x += labelPosition.width;
                    EditorGUI.PropertyField(position,
                        property.FindPropertyRelative(PropertiesEnum.randomMin.ToString()), GUIContent.none);
                    position.y += FlowEntConstants.SpacedSingleLineHeight;
                    EditorGUI.PropertyField(position,
                        property.FindPropertyRelative(PropertiesEnum.randomMax.ToString()), GUIContent.none);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawDropdown(Rect position, SerializedProperty property)
        {
            position.height = EditorGUIUtility.singleLineHeight;

            Rect menuPosition = position;
            menuPosition.x = position.xMax - MenuWidth;
            menuPosition.width = MenuWidth;

            if (GUI.Button(menuPosition, Icon.Select, Icon.Style))
            {
                GenericMenu context = new GenericMenu();

                SerializedProperty typeProperty = property.FindPropertyRelative(PropertiesEnum.type.ToString());

                foreach (DynamicValueType type in Enum.GetValues(typeof(DynamicValueType)))
                {
                    int intType = (int)type;
                    context.AddItem(new GUIContent(
                            type.ToString()),
                        () =>
                        {
                            typeProperty.enumValueIndex = intType;
                            property.serializedObject.ApplyModifiedProperties();
                        },
                        typeProperty.enumValueIndex == (int)type);
                }

                context.ShowAsContext();
            }
        }
    }
}