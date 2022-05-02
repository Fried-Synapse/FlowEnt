using System;
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
        public class Data
        {
            public TAnimation PreviewAnimation { get; set; }
            public float PreviewTime { get; set; }
            public bool IsInPreview => PreviewAnimation != null;

            public void Reset()
            {
                PreviewTime = 0;
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
        protected abstract void OnAnimationUpdated(Data data, float t);

        private void OnPreviewUpdate(SerializedProperty property)
        {
            foreach (UnityEditor.Editor item in ActiveEditorTracker.sharedTracker.activeEditors)
            {
                if (item.serializedObject == property?.serializedObject)
                {
                    item.Repaint();
                    return;
                }
            }
            PreviewController.Stop(false);
        }

        protected void StartPreview(SerializedProperty property)
        {
            Data data = GetData(property);
            data.PreviewAnimation = property.GetValue<TAnimationBuilder>().Build();
            data.PreviewAnimation.OnUpdated(t => OnAnimationUpdated(data, t));
            PreviewController.Start(
                new PreviewOptions(data.PreviewAnimation)
                {
                    OnUpdate = () => OnPreviewUpdate(property),
                    OnStop = data.Reset,
                });
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

            TAnimationBuilder animation = property.GetValue<TAnimationBuilder>();
            SerializedProperty parentProperty = property.GetParentArray();
            string name = animation.GetPropertyValue<object>("Options").GetPropertyValue<string>("Name");
            label.text = $"{(parentProperty == null ? label.text : "")} [{animation.GetType().Name.Replace("Builder", "")}{(string.IsNullOrEmpty(name) ? "" : $" - {name}")}]";

            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded, label);

            if (data.IsInPreview)
            {
                ColorUtility.TryParseHtmlString(FlowEntConstants.Preview, out Color previewColour);
                EditorGUI.DrawRect(position, previewColour);
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
                    PreviewController.Stop();
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

        protected float DrawControlButtons(Rect position, SerializedProperty property)
        {
            Data data = GetData(property);
            position = EditorGUI.IndentedRect(position);
            position.x -= 10f;
            position.width = EditorGUIUtility.singleLineHeight;

            IControllable controllable = data.PreviewAnimation;

            GUI.enabled = controllable != null;
            if (GUI.Button(position, Icon.PrevFrame, Icon.Style))
            {
                controllable.ChangeFrame(-1);
            }
            position.x += EditorGUIUtility.singleLineHeight;
            GUI.enabled = true;

            if (controllable?.PlayState == PlayState.Playing)
            {
                if (GUI.Button(position, Icon.Pause, Icon.Style))
                {
                    controllable.Pause();
                }
            }
            else
            {
                if (GUI.Button(position, Icon.Play, Icon.Style))
                {
                    if (controllable != null)
                    {
                        controllable.Resume();
                    }
                    else
                    {
                        StartPreview(property);
                    }
                }
            }
            position.x += EditorGUIUtility.singleLineHeight;

            GUI.enabled = controllable != null;
            if (GUI.Button(position, Icon.NextFrame, Icon.Style))
            {
                controllable.ChangeFrame(1);
            }
            position.x += EditorGUIUtility.singleLineHeight;

            if (GUI.Button(position, Icon.Stop, Icon.Style))
            {
                PreviewController.Stop();
            }
            GUI.enabled = true;

            return EditorGUIUtility.singleLineHeight * 4;
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
