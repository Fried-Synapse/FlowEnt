using System;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractEventsBuilderPropertyDrawer<TPropertiesEnum> : AbstractPropertiesBuilderPropertyDrawer<TPropertiesEnum>
        where TPropertiesEnum : Enum
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => property.isExpanded ? FlowEntConstants.SpacedSingleLineHeight + (PropertyHeight * Properties.Count) : EditorGUIUtility.singleLineHeight;

        protected override float PropertyHeight => 100f;

        protected override void DrawProperties(Rect position, SerializedProperty property)
        {
            position.y += FlowEntConstants.SpacedSingleLineHeight;
            for (int i = 0; i < Properties.Count; i++)
            {
                TPropertiesEnum prop = Properties[i];
                Rect propertyPosition = FlowEntEditorGUILayout.GetRect(position, i, PropertyHeight, PropertyHeight);
                EditorGUI.PropertyField(propertyPosition, property.FindPropertyRelative(prop.ToString()));
            }
        }
    }
}
