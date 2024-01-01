using System;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal interface ICrudable<T>
    {
        public T Clipboard { get; set; }
    }
    
    public static class GenericMenuExtensions 
    {
       internal static void AddListClear(this GenericMenu context, SerializedProperty listProperty)
        {
            SerializedProperty target = listProperty.Copy();

            void clear()
            {
                target.PersistentClearArray();
            }

            context.AddItem(new GUIContent("Clear"), clear);
        }

        internal static void AddListCrud<T>(this GenericMenu context, SerializedProperty listProperty, int index,
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

        internal static void AddCrud<T>(this GenericMenu context, SerializedProperty property, string name,
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

        internal static void AddExpand(this GenericMenu context, SerializedProperty listProperty)
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
        
        internal static void AddShowDisplayName(this GenericMenu context, SerializedProperty property)
        {
            SerializedProperty displayNameProperty = property.FindPropertyRelative("displayName");
            SerializedProperty isEnabledProperty = displayNameProperty.FindPropertyRelative("isEnabled");

            void showRename()
            {
                isEnabledProperty.boolValue = !isEnabledProperty.boolValue;
                isEnabledProperty.serializedObject.ApplyModifiedProperties();
            }

            context.AddItem(new GUIContent("Show Rename"), showRename, false, isEnabledProperty.boolValue);
        }
    }
}
