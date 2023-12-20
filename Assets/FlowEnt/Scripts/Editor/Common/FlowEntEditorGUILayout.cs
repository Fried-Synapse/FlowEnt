using System;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal interface ICrudable<T>
    {
        public T Clipboard { get; set; }
    }

    internal static class FlowEntEditorGUILayout
    {
        internal static void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
        {
            SerializedProperty copy = property.Copy();
            int baseDepth = copy.depth;
            copy.NextVisible(true);
            do
            {
                if (copy.depth <= baseDepth)
                {
                    break;
                }

                predicate(copy);
            } while (copy.NextVisible(false));
        }

        internal static void ShowListClear(GenericMenu context, SerializedProperty listProperty)
        {
            SerializedProperty target = listProperty.Copy();

            void clear()
            {
                target.PersistentClearArray();
            }

            context.AddItem(new GUIContent("Clear"), clear);
        }

        internal static void ShowListCrud<T>(GenericMenu context, SerializedProperty listProperty, int index,
            string name, ICrudable<T> crudable)
        {
            SerializedProperty target = listProperty.Copy();
            object item = target.GetArrayElementAtIndex(index).GetValue<object>();
            context.AddItem(new GUIContent($"Remove {name}"), () => target.PersistentDeleteArrayElementAtIndex(index));

            void copy()
            {
                crudable.Clipboard = (T)Activator.CreateInstance(item.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(item, crudable.Clipboard);
            }

            context.AddItem(new GUIContent($"Copy {name}"), copy);

            void pasteValues()
            {
                object copy = (T)Activator.CreateInstance(crudable.Clipboard.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(crudable.Clipboard, copy);
                target.PersistentSetArrayElementAtIndex(index, copy);
            }

            context.AddItem(new GUIContent($"Paste {name} Values"), pasteValues,
                crudable.Clipboard == null || item.GetType() != crudable.Clipboard.GetType());

            void pasteAsNew()
            {
                object copy = (T)Activator.CreateInstance(crudable.Clipboard.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(crudable.Clipboard, copy);
                target.PersistentInsertArrayElementAtIndex(target.arraySize, copy);
            }

            context.AddItem(new GUIContent($"Paste {name} as new"), pasteAsNew, crudable.Clipboard == null);
        }

        internal static void ShowCrud<T>(GenericMenu context, SerializedProperty property, string name,
            ICrudable<T> crudable)
        {
            SerializedProperty target = property.Copy();
            object item = target.GetValue<object>();

            void copy()
            {
                crudable.Clipboard = (T)Activator.CreateInstance(item.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(item, crudable.Clipboard);
            }

            context.AddItem(new GUIContent($"Copy {name}"), copy);

            void pasteValues()
            {
                object copy = (T)Activator.CreateInstance(crudable.Clipboard.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(crudable.Clipboard, copy);
                target.PersistentSetValue(copy);
            }

            context.AddItem(new GUIContent($"Paste {name} Values"), pasteValues,
                crudable.Clipboard == null || item.GetType() != crudable.Clipboard.GetType());
        }

        internal static void ShowExpand(GenericMenu context, SerializedProperty listProperty)
        {
            SerializedProperty target = listProperty.Copy();

            void expandAll()
            {
                target.ExpandArray(true);
            }
            
            void collapseAll()
            {
                target.ExpandArray(false);
            }

            context.AddItem(new GUIContent("Expand all"), expandAll);
            context.AddItem(new GUIContent("Collapse all"), collapseAll);
        }

        internal static Rect GetRect(Rect position, int index)
            => GetRect(position, index, FlowEntConstants.SpacedSingleLineHeight, EditorGUIUtility.singleLineHeight);

        internal static Rect GetRect(Rect position, int index, float spacedLineHeight, float lineHeight)
            => new Rect(position.x, position.y + (index * spacedLineHeight), position.width, lineHeight);

        internal static void PropertyFieldSingleLine(ref Rect position, SerializedProperty child)
        {
            position.height = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, child);
            position.y += FlowEntConstants.SpacedSingleLineHeight;
        }

        internal static void PropertyFieldIsEnabled(Rect position, SerializedProperty property)
        {
            Rect isEnabledPosition = position;
            isEnabledPosition.x += 2f;
            isEnabledPosition.width = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(isEnabledPosition,
                property.FindPropertyRelative(IdentifiableBuilderFields.IsEnabled), GUIContent.none);
        }
    }
}