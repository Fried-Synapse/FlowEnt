using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(IMotionBuilder), true)]
    public class AbstractMotionBuilderPropertyDrawer : PropertyDrawer, ICrudable<IMotionBuilder>
    {
        private readonly List<string> hiddenProperties = new()
        {
            "isEnabled",
        };

        private static IMotionBuilder clipboard;

        public IMotionBuilder Clipboard { get => clipboard; set => clipboard = value; }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight + 12;

            ForEachVisibleProperty(property, p => height += EditorGUI.GetPropertyHeight(p, true));
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect headerPosition = FlowEntEditorGUILayout.GetRectForLine(position, 0);
            IMotionBuilder motionBuilder = property.GetValue<IMotionBuilder>();
            Type motionBuilderType = motionBuilder.GetType();

            property.isExpanded = EditorGUI.Foldout(headerPosition, property.isExpanded, new GUIContent());

            Rect isEnabledPosition = headerPosition;
            isEnabledPosition.x += FlowEntConstants.PaddingFix;
            FlowEntEditorGUILayout.PropertyFieldIsEnabled(isEnabledPosition, property);

            //HACK: EditorGUI.Foldout doesn't know how to do overflows so we just draw the label separately
            string name = MotionNames.GetNames(motionBuilderType, motionBuilder).Preferred;
            label.text = name.PadLeft(name.Length + 10);
            EditorGUI.LabelField(headerPosition, label);

            DrawMenu(headerPosition, property);

            if (!property.isExpanded)
            {
                return;
            }

            position.y += FlowEntConstants.SpacedSingleLineHeight;

            ForEachVisibleProperty(property, p =>
            {
                float height = EditorGUI.GetPropertyHeight(p, true) + FlowEntConstants.DrawerSpacing;
                position.height = height;
                EditorGUI.PropertyField(position, p, true);
                if (p.propertyType == SerializedPropertyType.ObjectReference)
                {
                    Type attributeType = typeof(AutoAssignButtonMotionFieldAttribute);
                    FieldInfo fieldInfo = p.GetFieldInfo();
                    if (fieldInfo.GetCustomAttributes(attributeType, false).Length > 0)
                    {
                        AutoAssignButtonAttributeDrawer.DrawButton(position, p, fieldInfo);
                    }
                }

                position.y += height;
            });
        }

        private void DrawMenu(Rect position, SerializedProperty property)
        {
            Rect menuPosition = position;
            const float menuWidth = 20f;
            menuPosition.x = position.xMax - (menuWidth / 2f) - 10;
            menuPosition.width = menuWidth;
            menuPosition.height = EditorGUIUtility.singleLineHeight;
            if (GUI.Button(menuPosition, Icon.Menu, Icon.Style))
            {
                SerializedProperty parentProperty = property.GetParentArray();
                GenericMenu context = new();
                context.AddListCrud(parentProperty, parentProperty.GetArrayElementIndex(property), "Motion", this);
                context.AddSeparator(string.Empty);
                context.AddShowDisplayName(property);
                if (property.GetValue<IMotionBuilder>() is IGizmoDrawer)
                {
                    context.AddShowGizmoOptions(property);
                }

                context.ShowAsContext();
            }
        }

        private void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
        {
            FlowEntEditorGUILayout.ForEachVisibleProperty(property, p =>
            {
                if (!hiddenProperties.Contains(p.name))
                {
                    predicate(p);
                }
            });
        }
    }
}