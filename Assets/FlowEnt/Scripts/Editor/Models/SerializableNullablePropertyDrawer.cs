using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(SerializableNullable<>))]
    public class SerializableNullablePropertyDrawer : PropertyDrawer
    {
        private enum FieldsEnum
        {
            value,
            hasValue,
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property.FindPropertyRelative(FieldsEnum.value.ToString()), label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            Rect hasValuePosition = new Rect(position.x + EditorGUIUtility.labelWidth - 30, position.y, 15,
                FlowEntConstants.SpacedSingleLineHeight);
            SerializedProperty hasValueProperty = property.FindPropertyRelative(FieldsEnum.hasValue.ToString());
            EditorGUI.PropertyField(hasValuePosition, hasValueProperty, GUIContent.none);
            bool guiEnabled = GUI.enabled;
            GUI.enabled = guiEnabled && hasValueProperty.boolValue;
            position.height -= FlowEntConstants.DrawerSpacing;
            EditorGUI.PropertyField(position, property.FindPropertyRelative(FieldsEnum.value.ToString()), label);
            GUI.enabled = guiEnabled;
            EditorGUI.EndProperty();
        }
    }
}