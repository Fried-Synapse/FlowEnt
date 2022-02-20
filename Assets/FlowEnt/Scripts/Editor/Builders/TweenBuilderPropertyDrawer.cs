using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuilder))]
    public class TweenBuilderPropertyDrawer : PropertyDrawer
    {
        private static class Icon
        {
            public static GUIContent Play = EditorGUIUtility.IconContent("PlayButton@2x", "Play");
            public static GUIContent Pause = EditorGUIUtility.IconContent("PauseButton@2x", "Pause");
        }

        private readonly List<string> visibleProperties = new List<string>{
            "options",
            "events",
            "motions",
        };
        private float previewTime;
        private Tween previewTween;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight * 2;
            ForEachVisibleProperty(property, p => height += EditorGUI.GetPropertyHeight(p, true));
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded, label);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            DrawControls(FlowEntEditorGUILayout.GetRect(position, 1), property);
            position.y += FlowEntConstants.SpacedSingleLineHeight * 2;
            ForEachVisibleProperty(property, p =>
            {
                float height = EditorGUI.GetPropertyHeight(p, true) + FlowEntConstants.DrawerSpacing;
                position.height = height;
                EditorGUI.PropertyField(position, p, true);
                position.y += height;
            });
            EditorGUI.indentLevel--;
        }

        private void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
        {
            int baseDepth = property.depth;
            property.NextVisible(true);
            do
            {
                if (property.depth == baseDepth)
                {
                    break;
                }
                if (visibleProperties.Contains(property.name))
                {
                    predicate(property);
                }
            }
            while (property.NextVisible(false));
        }

        private void DrawControls(Rect position, SerializedProperty property)
        {
            float playButtonWidth = EditorGUIUtility.singleLineHeight;
            Rect playButtonPosition = position;
            playButtonPosition.width = playButtonWidth;
            if (GUI.Button(playButtonPosition, Icon.Play))
            {
                previewTween = property.GetValue<TweenBuilder>().Build();
                previewTween.OnUpdating(t => previewTime = t);
                previewTween.Start();
            }

            Rect progressPosition = position;
            progressPosition.width -= playButtonWidth;
            progressPosition.x += playButtonWidth;

            previewTime = EditorGUI.Slider(progressPosition, previewTime, 0f, 1f);
        }
    }
}
