using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenOptionsBuilder))]
    public class TweenOptionsBuilderPropertyDrawer : PropertyDrawer
    {
        private const float Spacing = 2f;
        private enum PropertiesEnum
        {
            name,
            autoStart,
            skipFrames,
            delay,
            timeScale,
            time,
            easing,
            loopCount,
            loopType
        }

        private List<PropertiesEnum> properties;
        private List<PropertiesEnum> Properties => properties ??= Enum.GetValues(typeof(PropertiesEnum)).Cast<PropertiesEnum>().ToList();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
                    => property.isExpanded ? (EditorGUIUtility.singleLineHeight + Spacing) * (Properties.Count + 1) : EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(GetRect(position, 0), property.isExpanded, label);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            for (int i = 0; i < Properties.Count; i++)
            {
                PropertiesEnum prop = Properties[i];
                switch (prop)
                {
                    case PropertiesEnum.loopCount:
                        DrawLoopCount(position, property, i);
                        break;
                    default:
                        EditorGUI.PropertyField(GetRect(position, i + 1), property.FindPropertyRelative(prop.ToString()));
                        break;
                }
            }
            EditorGUI.indentLevel--;
        }

        private void DrawLoopCount(Rect position, SerializedProperty property, int i)
        {
            Rect loopCountRect = GetRect(position, i + 1);
            loopCountRect.width /= 2f;

            SerializedProperty isLoopCountInfiniteProperty = property.FindPropertyRelative("isLoopCountInfinite");

            GUI.enabled = !isLoopCountInfiniteProperty.boolValue;
            EditorGUI.PropertyField(loopCountRect, property.FindPropertyRelative(PropertiesEnum.loopCount.ToString()));
            GUI.enabled = true;

            loopCountRect.x += loopCountRect.width;
            EditorGUI.PropertyField(loopCountRect, isLoopCountInfiniteProperty);
        }

        private Rect GetRect(Rect position, int index)
            => new Rect(position.x, position.y + (index * (EditorGUIUtility.singleLineHeight + Spacing)), position.width, EditorGUIUtility.singleLineHeight);
    }
}
