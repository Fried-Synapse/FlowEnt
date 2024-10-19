using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class AbstractAnimationBuilderPropertyDrawer
    {
        internal static bool IsPreviewDisabled { get; set; }
        internal static string FocusedPropertyId { get; set; }
    }

    public abstract class AbstractAnimationBuilderPropertyDrawer<TAnimation, TAnimationBuilder> : PropertyDrawer,
        ICrudable<TAnimationBuilder>
        where TAnimation : AbstractAnimation
        where TAnimationBuilder : AbstractAnimationBuilder<TAnimation>
    {
        protected virtual List<string> VisibleProperties => new List<string>
        {
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
            property.FindPropertyRelative("hierarchy").stringValue = property.GetHierarchy();
            
            Rect headerPosition = FlowEntEditorGUILayout.GetRectForLine(position, 0);
            TAnimationBuilder animation = property.GetValue<TAnimationBuilder>();
            SerializedProperty parentProperty = property.GetParentArray();
            string name = animation.GetPropertyValue<object>("Options").GetPropertyValue<string>("Name");
            name = $"{(parentProperty == null ? label.text : "")} " +
                   $"[{animation.GetType().Name.Replace("Builder", "")}" +
                   $"{(string.IsNullOrEmpty(name) ? "" : $" - {name}")}]";

            if (parentProperty != null)
            {
                FlowEntEditorGUILayout.PropertyFieldIsEnabled(headerPosition, property);
                name = name.PadLeft(name.Length + 6);
            }

            label.text = name;
            property.isExpanded = EditorGUI.Foldout(headerPosition, property.isExpanded, label);

            DrawMenu(headerPosition, property);

            if (!property.isExpanded)
            {
                return;
            }

            using (EditorGUI.ChangeCheckScope check = new())
            {
                position.y += EditorGUIUtility.singleLineHeight;

                ForEachVisibleProperty(property, p =>
                {
                    float height = EditorGUI.GetPropertyHeight(p, true) + FlowEntConstants.DrawerSpacing;
                    position.height = height;
                    EditorGUI.PropertyField(position, p, true);
                    position.y += height;
                });

                if (check.changed && !AbstractAnimationBuilderPropertyDrawer.IsPreviewDisabled && PreviewerWindow.IsAvailable)
                {
                    property.serializedObject.ApplyModifiedProperties();
                    if (AbstractAnimationBuilderPropertyDrawer.FocusedPropertyId == property.GetUniqueId())
                    {
                        PreviewerWindow.Instance.FocusAnimation(property.GetValue<IAbstractAnimationBuilder>());
                    }
                    else
                    {
                        PreviewerWindow.Instance.RefreshAnimations();
                    }

                    GUIUtility.ExitGUI();
                }
            }
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
                AbstractAnimationBuilderPropertyDrawer.IsPreviewDisabled = true;
                EditorApplication.delayCall += () => { AbstractAnimationBuilderPropertyDrawer.IsPreviewDisabled = false; };
                PreviewerWindow.ShowSingleton();
                AbstractAnimationBuilderPropertyDrawer.FocusedPropertyId = property.GetUniqueId();
                PreviewerWindow.Instance.FocusAnimation(property.GetValue<IAbstractAnimationBuilder>());
            }

            Rect menuPosition = position;
            menuPosition.x = position.xMax - menuWidth;
            menuPosition.width = menuWidth;

            if (GUI.Button(menuPosition, Icon.Menu, Icon.Style))
            {
                SerializedProperty parentProperty = property.GetParentArray();
                GenericMenu context = new();
                if (parentProperty == null)
                {
                    context.AddCrud(property, "Animation", this);
                }
                else
                {
                    context.AddListCrud(parentProperty, parentProperty.GetArrayElementIndex(property), "Animation", this);
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