using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractListBuilderPropertyDrawer<TListItem> : PropertyDrawer
        where TListItem : IBuilderListItem
    {
        protected virtual void DrawMenu(Rect position, SerializedProperty property) { }
        protected virtual GUIContent GetLabel(SerializedProperty property, GUIContent label) => label;
        protected abstract Rect Draw(Rect position, SerializedProperty property);

        private readonly Queue<TListItem> addedItemTypes = new Queue<TListItem>();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight * 2;
            SerializedProperty listProperty = property.FindPropertyRelative("items");
            for (int i = 0; i < listProperty.arraySize; i++)
            {
                SerializedProperty listItemProperty = listProperty.GetArrayElementAtIndex(i);
                height += EditorGUI.GetPropertyHeight(listItemProperty, true);
            }
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded, GetLabel(property, label), EditorStyles.foldoutHeader);

            Rect headerPosition = FlowEntEditorGUILayout.GetRect(position, 0);
            DrawMenu(headerPosition, property);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            position = Draw(FlowEntEditorGUILayout.GetRect(position, 1), property);
            DrawList(FlowEntEditorGUILayout.GetRect(position, 1), property);
            EditorGUI.indentLevel--;
        }

        protected void DrawButton(Rect position, string name, Action callback)
        {
            if (GUI.Button(EditorGUI.IndentedRect(position), name))
            {
                callback();
            }
        }

        private void DrawList(Rect position, SerializedProperty property)
        {
            SerializedProperty listProperty = property.FindPropertyRelative("items");
            List<TListItem> list = listProperty.GetValue<List<TListItem>>();

            while (addedItemTypes.Count > 0)
            {
                TListItem builder = addedItemTypes.Dequeue();
                list.Add(builder);
            }

            for (int i = 0; i < listProperty.arraySize; i++)
            {
                SerializedProperty listItemProperty = listProperty.GetArrayElementAtIndex(i);
                float height = EditorGUI.GetPropertyHeight(listItemProperty, true);
                position.height = height;
                EditorGUI.PropertyField(position, listItemProperty, true);
                position.y += height;
            }
        }

        protected void AddItem(TListItem item)
        {
            addedItemTypes.Enqueue(item);
        }
    }
}