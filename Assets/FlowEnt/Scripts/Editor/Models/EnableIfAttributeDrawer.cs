using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    public class EnableIfAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnableIfAttribute enableIfAttribute = (EnableIfAttribute)attribute;
            SerializedProperty controlProperty = property.GetParent().FindPropertyRelative(enableIfAttribute.Field);

            bool guiState = GUI.enabled;
            bool fieldState = guiState;

            if (controlProperty == null)
            {
                Debug.LogWarning($"[DisableIf] Cannot find field with name {enableIfAttribute.Field}.",
                    property.serializedObject.targetObject);
            }
            else
            {
                fieldState = controlProperty.boolValue != enableIfAttribute.IsInverted;
            }

            GUI.enabled = fieldState;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = guiState;
        }
    }
}