using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public abstract class AbstractListBuilderPropertyDrawer<TListItem>
        : PropertyDrawer<AbstractListBuilderPropertyDrawer<TListItem>.Data>
        where TListItem : IListBuilderItem
    {
        public class Data
        {
            public Queue<TListItem> AddedItemTypes { get; } = new();
        }

        private const string ItemsNames = "items";

        private Dictionary<string, ReorderableList> Lists { get; } = new();

        protected virtual GUIContent GetLabel(SerializedProperty property, GUIContent label) => label;

        protected virtual void AddItemsToContextMenu(GenericMenu context, SerializedProperty property)
        {
        }

        protected virtual void Draw(ref Rect position, SerializedProperty property)
        {
        }

        protected abstract void OnAdd(Rect buttonRect, ReorderableList list);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight;
            if (Lists.TryGetValue(property.FindPropertyRelative(ItemsNames).propertyPath, out ReorderableList list))
            {
                height += list?.GetHeight() ?? 0;
            }

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

            position = FlowEntEditorGUILayout.GetRect(position, 1);
            Draw(ref position, property);
            DrawList(position, property);
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
                GenericMenu context = new();
                context.AddListClear(property.FindPropertyRelative(ItemsNames));
                context.AddSeparator(string.Empty);
                context.AddExpand(property.FindPropertyRelative(ItemsNames));
                context.AddSeparator(string.Empty);
                AddItemsToContextMenu(context, property);
                context.ShowAsContext();
            }
        }

        private void DrawList(Rect position, SerializedProperty property)
        {
            const int padding = 10;

            Data data = GetData(property);
            SerializedProperty listProperty = property.FindPropertyRelative(ItemsNames);
            if (!Lists.TryGetValue(listProperty.propertyPath, out ReorderableList list))
            {
                list = new ReorderableList(listProperty.serializedObject, listProperty,
                    true, true, true, true);
                Lists.Add(listProperty.propertyPath, list);
            }

            while (data.AddedItemTypes.Count > 0)
            {
                listProperty.PersistentInsertArrayElementAtIndex(listProperty.arraySize, data.AddedItemTypes.Dequeue());
            }

            list.headerHeight = 0;
            list.drawElementCallback = drawElement;
            list.elementHeightCallback = getElementHeight;
            list.onAddDropdownCallback = OnAdd;
            position.x += padding;
            position.width -= padding;
            list.DoList(position);
            
            void drawElement(Rect rect, int index, bool isActive, bool isFocused)
            {
                rect.x += padding;
                rect.width -= padding;
                EditorGUI.PropertyField(rect, listProperty.GetArrayElementAtIndex(index), true);
            }

            float getElementHeight(int index)
                => EditorGUI.GetPropertyHeight(listProperty.GetArrayElementAtIndex(index), true);
        }
    }
}