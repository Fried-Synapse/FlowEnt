using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractAnimationBuilderPropertyDrawer<TAnimation, TAnimationBuilder> : PropertyDrawer,
        ICrudable<TAnimationBuilder>
        where TAnimation : AbstractAnimation
        where TAnimationBuilder : AbstractAnimationBuilder<TAnimation>
    {
        protected virtual List<string> VisibleProperties => new List<string>{
            "options",
            "events",
            "motions",
        };

        private static TAnimationBuilder clipboard;
        public TAnimationBuilder Clipboard { get => clipboard; set => clipboard = value; }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight + FlowEntConstants.DrawerSpacing;
            ForEachVisibleProperty(property, p => height += EditorGUI.GetPropertyHeight(p, true));
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            TAnimationBuilder animation = property.GetValue<TAnimationBuilder>();
            SerializedProperty parentProperty = property.GetParentArray();
            string name = animation.GetPropertyValue<object>("Options").GetPropertyValue<string>("Name");
            label.text = $"{(parentProperty == null ? label.text : "")} [{animation.GetType().Name.Replace("Builder", "")}{(string.IsNullOrEmpty(name) ? "" : $" - {name}")}]";

            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded, label);

            DrawMenu(position, property);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;

            using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope())
            {
                position.y += EditorGUIUtility.singleLineHeight;
                ForEachVisibleProperty(property, p =>
                {
                    float height = EditorGUI.GetPropertyHeight(p, true) + FlowEntConstants.DrawerSpacing;
                    position.height = height;
                    EditorGUI.PropertyField(position, p, true);
                    position.y += height;
                });

                if (check.changed)
                {
                    OldFlowEntPreviewerWindow.Instance?.ResetAnimations();
                }
            }

            EditorGUI.indentLevel--;
        }

        private void DrawMenu(Rect position, SerializedProperty property)
        {
            const float previewWidth = 60f;
            const float menuWidth = 20f;
            position.height = EditorGUIUtility.singleLineHeight;

            Rect previewPosition = position;
            previewPosition.x = position.xMax - menuWidth - previewWidth;
            previewPosition.width = previewWidth;

            if (GUI.Button(previewPosition, "Preview"))
            {
                FlowEntMenu.ShowPreviewer();
            }

            Rect menuPosition = position;
            menuPosition.x = position.xMax - menuWidth;
            menuPosition.width = menuWidth;

            if (GUI.Button(menuPosition, Icon.Menu, Icon.Style))
            {
                SerializedProperty parentProperty = property.GetParentArray();
                GenericMenu context = new GenericMenu();
                if (parentProperty == null)
                {
                    FlowEntEditorGUILayout.ShowCrud(context, property, "Animation", this);
                }
                else
                {
                    FlowEntEditorGUILayout.ShowListCrud(context, parentProperty, parentProperty.GetArrayElementIndex(property), "Animation", this);
                }
                context.ShowAsContext();
            }
        }

        private void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
        {
            FlowEntEditorGUILayout.ForEachVisibleProperty(property, p =>
            {
                if (VisibleProperties.Contains(p.name))
                {
                    predicate(p);
                }
            });
        }
    }
}
