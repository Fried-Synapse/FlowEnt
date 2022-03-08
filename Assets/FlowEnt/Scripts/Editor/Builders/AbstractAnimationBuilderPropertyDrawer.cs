using System;
using System.Collections;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractAnimationBuilderPropertyDrawer<TAnimation, TAnimationBuilder> : PropertyDrawer<AbstractAnimationBuilderPropertyDrawer<TAnimation, TAnimationBuilder>.Data>,
        ICrudable<TAnimationBuilder>
        where TAnimation : AbstractAnimation
        where TAnimationBuilder : AbstractAnimationBuilder<TAnimation>
    {
        public class Data : IPreviewable
        {
            public TAnimation PreviewAnimation { get; set; }
            AbstractAnimation IPreviewable.Animation => PreviewAnimation;
            public float PreviewTime { get; set; }
            public bool IsInPreview => PreviewAnimation != null;
            public SerializedObject SerializedObject { get; set; }

            public void Reset()
            {
                PreviewTime = 0;
                PreviewAnimation?.Stop();
                PreviewAnimation = null;
            }
        }

        protected virtual List<string> VisibleProperties => new List<string>{
            "options",
            "events",
            "motions",
        };

        private static TAnimationBuilder clipboard;
        public TAnimationBuilder Clipboard { get => clipboard; set => clipboard = value; }

        protected abstract void DrawControls(Rect position, SerializedProperty property);
        protected abstract TAnimation Build(SerializedProperty property);
        protected abstract void OnAnimationUpdated(Data data, float t);

        protected void StartPreview(SerializedProperty property)
        {
            Data data = GetData(property);
            data.PreviewAnimation = Build(property);
            data.PreviewAnimation.OnUpdated(t =>
            {
                OnAnimationUpdated(data, t);

                foreach (UnityEditor.Editor item in ActiveEditorTracker.sharedTracker.activeEditors)
                {
                    if (item.serializedObject == property?.serializedObject)
                    {
                        item.Repaint();
                        break;
                    }
                }
            });
            PreviewController.StartPreview(data);
            try
            {
                data.PreviewAnimation.Start();
            }
            catch (Exception ex)
            {
                FlowEntDebug.LogError(
                    $"<color={FlowEntConstants.Red}><b>Exception on starting animation</b></color>\n" +
                    $"<color={FlowEntConstants.Orange}><b>The preview animation is throwing an exception</b></color>:\n\n" +
                    $"<b>Exception</b>:\n{ex}");
                PreviewController.StopPreview();
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = (FlowEntConstants.SpacedSingleLineHeight * 2) + FlowEntConstants.DrawerSpacing;
            ForEachVisibleProperty(property, p => height += EditorGUI.GetPropertyHeight(p, true));
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Data data = GetData(property);
            data.SerializedObject = property.serializedObject;

            TAnimationBuilder animation = property.GetValue<TAnimationBuilder>();
            SerializedProperty parentProperty = property.GetParentArray();
            string name = animation.GetPropertyValue<object>("Options").GetPropertyValue<string>("Name");
            label.text = $"{(parentProperty == null ? label.text : "")} [{animation.GetType().Name.Replace("Builder", "")}{(string.IsNullOrEmpty(name) ? "" : $" - {name}")}]";

            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded, label);

            if (data.IsInPreview)
            {
                EditorGUI.DrawRect(position, new Color(1f, 0f, 0f, 0.2f));
            }

            DrawMenu(position, property);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;

            DrawControls(FlowEntEditorGUILayout.GetRect(position, 1), property);

            using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope())
            {
                position.y += FlowEntConstants.SpacedSingleLineHeight + EditorGUIUtility.singleLineHeight;
                ForEachVisibleProperty(property, p =>
                {
                    float height = EditorGUI.GetPropertyHeight(p, true) + FlowEntConstants.DrawerSpacing;
                    position.height = height;
                    EditorGUI.PropertyField(position, p, true);
                    position.y += height;
                });

                if (data.IsInPreview && check.changed)
                {
                    PreviewController.StopPreview();
                }
            }

            EditorGUI.indentLevel--;
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
                TAnimationBuilder animation = property.GetValue<TAnimationBuilder>();
                SerializedProperty parentProperty = property.GetParentArray();
                GenericMenu context = new GenericMenu();
                FlowEntEditorGUILayout.ShowCrud(context, parentProperty?.GetValue<IList>(), animation, "Animation", this);
                context.ShowAsContext();
            }
        }

        protected float DrawControlButtons(Rect position, SerializedProperty property)
        {
            Data data = GetData(property);
            position = EditorGUI.IndentedRect(position);
            position.x -= 10f;
            position.width = EditorGUIUtility.singleLineHeight;
            if (data.PreviewAnimation?.PlayState == PlayState.Playing)
            {
                if (GUI.Button(position, Icon.Pause, Icon.Style))
                {
                    data.PreviewAnimation.Pause();
                }
            }
            else
            {
                if (GUI.Button(position, Icon.Play, Icon.Style))
                {
                    if (data.PreviewAnimation?.PlayState == PlayState.Paused)
                    {
                        data.PreviewAnimation.Resume();
                    }
                    else
                    {
                        PreviewController.StopPreview();
                        StartPreview(property);
                    }
                }
            }
            position.x += EditorGUIUtility.singleLineHeight;
            if (GUI.Button(position, Icon.Stop, Icon.Style))
            {
                PreviewController.StopPreview();
            }
            return EditorGUIUtility.singleLineHeight * 2;
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
