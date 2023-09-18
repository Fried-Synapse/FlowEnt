using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class
        AbstractListBuilderPropertyDrawer<TListItem> : PropertyDrawer<AbstractListBuilderPropertyDrawer<TListItem>.Data>
        where TListItem : IListBuilderItem
    {
        public class Data
        {
            public Queue<TListItem> AddedItemTypes { get; } = new Queue<TListItem>();
        }

        protected virtual void DrawMenu(Rect position, SerializedProperty property)
        {
        }

        protected virtual GUIContent GetLabel(SerializedProperty property, GUIContent label) => label;
        protected abstract Rect Draw(Rect position, SerializedProperty property);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight * 2;
            //TODO get height of items from the reorderable list
            SerializedProperty listProperty = property.FindPropertyRelative("items");
            for (int i = 0; i < listProperty.arraySize; i++)
            {
                SerializedProperty listItemProperty = listProperty.GetArrayElementAtIndex(i);
                height += EditorGUI.GetPropertyHeight(listItemProperty, true);
            }

            height += 70;
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded,
                GetLabel(property, label), EditorStyles.foldoutHeader);

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

        private ReorderableList AnimationsList { get; set; }

        private void DrawList(Rect position, SerializedProperty property)
        {
            Data data = GetData(property);
            SerializedProperty listProperty = property.FindPropertyRelative("items");
            List<TListItem> list = listProperty.GetValue<List<TListItem>>();

            if (AnimationsList == null)
            {
                AnimationsList = new ReorderableList(list, typeof(TListItem),
                    true, true, true, true);
            }
            // AnimationsList.onAddCallback = reorderableList =>
            // {
            //     GenericMenu context = new GenericMenu();
            //     context.AddItem(new GUIContent("Flow"), false, () => ((IList)list).Add(new FlowBuilder()));
            //     context.AddItem(new GUIContent("Tween"), false, () => ((IList)list).Add(new TweenBuilder()));
            //     context.AddItem(new GUIContent("Echo"), false, () => ((IList)list).Add(new EchoBuilder()));
            //     position.y += 3f;
            //     context.DropDown(position);
            // };

            AnimationsList.drawElementCallback = DrawElement;
            AnimationsList.drawHeaderCallback = (rect) => { };
            AnimationsList.elementHeightCallback = GetElementHeight;
            AnimationsList.onReorderCallback = Reorder;
            AnimationsList.DoList(position);
            position.height += 300;

            while (data.AddedItemTypes.Count > 0)
            {
                listProperty.PersistentInsertArrayElementAtIndex(listProperty.arraySize, data.AddedItemTypes.Dequeue());
            }

            void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
            {
                SerializedProperty listItemProperty = listProperty.GetArrayElementAtIndex(index);
                float height = EditorGUI.GetPropertyHeight(listItemProperty, true);
                //rect.height = height;
                rect.x += 10;
                EditorGUI.PropertyField(rect, listItemProperty, true);
            }

            float GetElementHeight(int index)
            {
                SerializedProperty listItemProperty = listProperty.GetArrayElementAtIndex(index);
                return EditorGUI.GetPropertyHeight(listItemProperty, true);
            }

            void Reorder(ReorderableList list)
            {
            }

            return;

            while (data.AddedItemTypes.Count > 0)
            {
                listProperty.PersistentInsertArrayElementAtIndex(listProperty.arraySize, data.AddedItemTypes.Dequeue());
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
    }
}