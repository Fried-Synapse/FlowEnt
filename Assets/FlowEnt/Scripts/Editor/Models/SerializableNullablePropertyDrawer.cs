using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(SerializableNullable<>))]
    public class SerializableNullablePropertyDrawer : PropertyDrawer
    {
        private enum PropertiesEnum
        {
            value,
            hasValue,
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property.FindPropertyRelative(PropertiesEnum.value.ToString()), label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //EditorGUI.BeginProperty(position, label, property);
            Rect hasValuePosition = new Rect(position.x + EditorGUIUtility.labelWidth - 30, position.y, 15,
                FlowEntConstants.SpacedSingleLineHeight);
            SerializedProperty hasValueProperty = property.FindPropertyRelative(PropertiesEnum.hasValue.ToString());
            EditorGUI.PropertyField(hasValuePosition, hasValueProperty, GUIContent.none);
            bool guiEnabled = GUI.enabled;
            GUI.enabled = guiEnabled && hasValueProperty.boolValue;
            position.height -= FlowEntConstants.DrawerSpacing;
            EditorGUI.PropertyField(position, property.FindPropertyRelative(PropertiesEnum.value.ToString()), label);
            GUI.enabled = guiEnabled;
            //EditorGUI.EndProperty();

            //
            // // Don't make child fields be indented
            // var indent = EditorGUI.indentLevel;
            // EditorGUI.indentLevel = 0;
            //
            // // Calculate rects
            // var setRect = new Rect(position.x, position.y, 15, position.height);
            // var consumed = setRect.width + 5;
            // var valueRect = new Rect(position.x + consumed, position.y, position.width - consumed, position.height);
            //
            // // Draw fields - pass GUIContent.none to each so they are drawn without labels
            // var hasValueProp = property.FindPropertyRelative("hasValue");
            // EditorGUI.PropertyField(setRect, hasValueProp, GUIContent.none);
            // bool guiEnabled = GUI.enabled;
            // GUI.enabled = guiEnabled && hasValueProp.boolValue;
            // EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("v"), GUIContent.none);
            // GUI.enabled = guiEnabled;
            //
            // // Set indent back to what it was
            // EditorGUI.indentLevel = indent;
        }
    }
}