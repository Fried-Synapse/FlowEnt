using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractEventsBuilderPropertyDrawer : AbstractPropertiesBuilderPropertyDrawer<AbstractEventsBuilderPropertyDrawer.FieldsEnum>
    {
        public enum FieldsEnum
        {
            onStarting,
            onStarted,
            onUpdating,
            onUpdated,
            onLoopCompleted,
            onCompleting,
            onCompleted,
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => !property.isExpanded
                ? EditorGUIUtility.singleLineHeight
                : Properties.Sum(prop =>
                      EditorGUI.GetPropertyHeight(property.FindPropertyRelative(prop.ToString())) + FlowEntConstants.DrawerSpacing) +
                  EditorGUIUtility.singleLineHeight;

        protected override void DrawProperties(Rect position, SerializedProperty property)
        {
            position.y += FlowEntConstants.SpacedSingleLineHeight;
            foreach (FieldsEnum prop in Properties)
            {
                position.height = EditorGUI.GetPropertyHeight(property.FindPropertyRelative(prop.ToString())) + FlowEntConstants.DrawerSpacing;
                EditorGUI.PropertyField(position, property.FindPropertyRelative(prop.ToString()));
                position.y += position.height;
            }
        }
    }
}