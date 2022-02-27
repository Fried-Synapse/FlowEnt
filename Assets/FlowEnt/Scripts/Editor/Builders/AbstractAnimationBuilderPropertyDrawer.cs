using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractAnimationBuilderPropertyDrawer<TAnimation> : PropertyDrawer, IPreviewable
        where TAnimation : AbstractAnimation
    {
        protected static class Icon
        {
            public static GUIContent Play = EditorGUIUtility.IconContent("PlayButton@2x", "Play");
            public static GUIContent Pause = EditorGUIUtility.IconContent("PauseButton@2x", "Pause");
            public static GUIContent Stop = EditorGUIUtility.IconContent("PreMatQuad@2x", "Pause");
            public static GUIStyle Style = new GUIStyle(EditorStyles.miniButton) { padding = new RectOffset(2, 2, 2, 2) };
        }

        private readonly List<string> visibleProperties = new List<string>{
            "options",
            "events",
            "motions",
        };

        protected TAnimation previewAnimation;
        public AbstractAnimation PreviewAnimation => previewAnimation;
        public SerializedObject SerializedObject { get; private set; }
        protected virtual bool IsInPreview => previewAnimation != null;
        protected abstract void DrawControls(Rect position, SerializedProperty property);
        protected abstract TAnimation Build(SerializedProperty property);
        protected abstract void OnAnimationUpdated(float t);

        public virtual void Reset()
        {
            previewAnimation?.Stop();
            previewAnimation = null;
        }

        protected void StartPreview(SerializedProperty property)
        {
            previewAnimation = Build(property);
            previewAnimation.OnUpdated(t =>
            {
                OnAnimationUpdated(t);
                foreach (UnityEditor.Editor item in ActiveEditorTracker.sharedTracker.activeEditors)
                {
                    if (item.serializedObject == property?.serializedObject)
                    {
                        item.Repaint();
                        break;
                    }
                }
            });
            FlowEntEditorController.Instance.StartPreview(this);
            previewAnimation.Start();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = (FlowEntConstants.SpacedSingleLineHeight * 2) + 10;
            ForEachVisibleProperty(property, p => height += EditorGUI.GetPropertyHeight(p, true));
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedObject = property.serializedObject;
            Event ev = Event.current;
            if (ev.type == EventType.MouseDown && ev.button == 1 && position.Contains(ev.mousePosition))
            {
                GenericMenu context = new GenericMenu();
                context.AddItem(new GUIContent("Copy"), false, () => { });
                context.AddItem(new GUIContent("Paste"), false, () => { });
                context.ShowAsContext();
            }

            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded, label);

            if (IsInPreview)
            {
                EditorGUI.DrawRect(position, new Color(1f, 0f, 0f, 0.2f));
            }

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;

            DrawControls(FlowEntEditorGUILayout.GetRect(position, 1), property);
            using (EditorGUI.ChangeCheckScope check = new EditorGUI.ChangeCheckScope())
            {
                position.y += FlowEntConstants.SpacedSingleLineHeight * 2;
                ForEachVisibleProperty(property, p =>
                {
                    float height = EditorGUI.GetPropertyHeight(p, true) + FlowEntConstants.DrawerSpacing;
                    position.height = height;
                    EditorGUI.PropertyField(position, p, true);
                    position.y += height;
                });

                if (check.changed)
                {
                    FlowEntEditorController.Instance.StopPreview();
                }
            }

            EditorGUI.indentLevel--;
        }

        protected void DrawButtons(Rect position, SerializedProperty property)
        {
            position.width = EditorGUIUtility.singleLineHeight;
            position.x += 5;
            if (previewAnimation?.PlayState == PlayState.Playing)
            {
                if (GUI.Button(position, Icon.Pause, Icon.Style))
                {
                    previewAnimation.Pause();
                }
            }
            else
            {
                if (GUI.Button(position, Icon.Play, Icon.Style))
                {
                    if (previewAnimation?.PlayState == PlayState.Paused)
                    {
                        previewAnimation.Resume();
                    }
                    else
                    {
                        FlowEntEditorController.Instance.StopPreview();
                        StartPreview(property);
                    }
                }
            }
            position.x += EditorGUIUtility.singleLineHeight;
            if (GUI.Button(position, Icon.Stop, Icon.Style))
            {
                FlowEntEditorController.Instance.StopPreview();
            }
        }

        private void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
        {
            FlowEntEditorGUILayout.ForEachVisibleProperty(property, p =>
            {
                if (visibleProperties.Contains(p.name))
                {
                    predicate(p);
                }
            });
        }
    }
}
