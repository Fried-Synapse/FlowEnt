using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractPropertiesBuilderPropertyDrawer<TPropertiesEnum> : PropertyDrawer
        where TPropertiesEnum : Enum
    {
        protected abstract float PropertyHeight { get; }
        private List<TPropertiesEnum> properties;

        protected List<TPropertiesEnum> Properties =>
            properties ??= Enum.GetValues(typeof(TPropertiesEnum)).Cast<TPropertiesEnum>().ToList();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => property.isExpanded
                ? (PropertyHeight + FlowEntConstants.DrawerSpacing) * (Properties.Count + 1)
                : EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded,
                label, true, EditorStyles.foldoutHeader);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            DrawProperties(position, property);
            EditorGUI.indentLevel--;
        }

        protected abstract void DrawProperties(Rect position, SerializedProperty property);

        protected void DrawNullable(Rect position, SerializedProperty property, string propertyName,
            string flagPropertyName, bool isInverted = false)
        {
            float width = position.width;
            float labelWidth = EditorGUIUtility.labelWidth;

            position.width = width * 0.6f;
            EditorGUIUtility.labelWidth = labelWidth * 0.8f;

            SerializedProperty flagProperty = property.FindPropertyRelative(flagPropertyName);
            GUI.enabled = flagProperty.boolValue ^ isInverted;
            EditorGUI.PropertyField(position, property.FindPropertyRelative(propertyName));
            GUI.enabled = true;

            position.x += position.width - EditorGUIUtility.singleLineHeight;
            
            EditorGUI.PropertyField(position, flagProperty);
            
            EditorGUIUtility.labelWidth = labelWidth;
        }
    }
}