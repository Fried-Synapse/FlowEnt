using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractAnimationBuilderPropertyDrawer<TAnimation> : PropertyDrawer
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
        protected virtual bool IsInPreview => previewAnimation != null;
        protected abstract void DrawControls(Rect position, SerializedProperty property);
        protected abstract TAnimation Build(SerializedProperty property);
        protected abstract void OnAnimationUpdated(float t);

        protected virtual void Reset()
        {
            FlowEntEditorController.Instance.StopPreview();
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
                    Reset();
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
                        previewAnimation = Build(property);
                        previewAnimation.OnUpdated(t =>
                        {
                            OnAnimationUpdated(t);
                            EditorUtility.SetDirty(property.serializedObject.targetObject);
                        });
                        FlowEntEditorController.Instance.StartPreview(previewAnimation);
                        previewAnimation.Start();
                    }
                }
            }
            position.x += EditorGUIUtility.singleLineHeight;
            if (GUI.Button(position, Icon.Stop, Icon.Style))
            {
                Reset();
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
