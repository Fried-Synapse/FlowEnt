using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(AutoAssignButtonAttribute))]
    public class AutoAssignButtonAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property, label, true);

            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                return;
            }

            DrawButton(position, property, fieldInfo);
            EditorGUI.EndProperty();
        }

        public static void DrawButton(Rect position, SerializedProperty property, FieldInfo fieldInfo)
        {
            Rect buttonPosition = position;
            buttonPosition.width = buttonPosition.height;
            buttonPosition.x -= buttonPosition.width - 1;

            Component component = (Component)property.serializedObject.targetObject;
            bool guiState = GUI.enabled;
            bool fieldState = component != null;
            GUIContent icon = new(Icon.Locate)
            {
                tooltip = fieldState ? "Set current object" : "Cannot get component"
            };

            GUI.enabled = fieldState;
            if (GUI.Button(buttonPosition, icon, Icon.Style))
            {
                Object obj = fieldInfo.FieldType switch
                {
                    _ when typeof(GameObject) == fieldInfo.FieldType => component.gameObject,
                    _ when typeof(Component).IsAssignableFrom(fieldInfo.FieldType)
                        => component.GetComponent(fieldInfo.FieldType),
                    _ => null
                };
                if (obj == null)
                {
                    EditorUtility.DisplayDialog(
                        "Component not found",
                        $"Cannot find component of type [{fieldInfo.FieldType.Name}]",
                        "Ok");
                }
                else
                {
                    property.objectReferenceValue = obj;
                }
            }

            GUI.enabled = guiState;
        }
    }
}