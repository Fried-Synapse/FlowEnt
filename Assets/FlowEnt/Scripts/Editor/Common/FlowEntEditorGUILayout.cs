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
        private static GUIStyle labelStyle;
        internal static GUIStyle LabelStyle
        {
            get
            {
                if (labelStyle == null)
                {
                    labelStyle = new GUIStyle(EditorStyles.label)
                    {
                        richText = true,
                        wordWrap = true,
                    };
                }
                return labelStyle;
            }
        }

        internal static void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
        {
            int baseDepth = property.depth;
            property.NextVisible(true);
            do
            {
                if (property.depth <= baseDepth)
                {
                    break;
                }

                predicate(property);
            }
            while (property.NextVisible(false));
        }

        internal static void ShowListCrud<T>(GenericMenu context, SerializedProperty listProperty, int index, string name, ICrudable<T> crudable)
        {
            SerializedProperty target = listProperty.Copy();
            object item = target.GetArrayElementAtIndex(index).GetValue<object>();
            context.AddItem(new GUIContent($"Remove {name}"), () => target.PersistentDeleteArrayElementAtIndex(index));
            void copyMotion()
            {
                crudable.Clipboard = (T)Activator.CreateInstance(item.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(item, crudable.Clipboard);
            }
            context.AddItem(new GUIContent($"Copy {name}"), copyMotion);
            void pasteMotionValues()
            {
                object copy = (T)Activator.CreateInstance(crudable.Clipboard.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(crudable.Clipboard, copy);
                target.PersistentSetArrayElementAtIndex(index, copy);
            }
            context.AddItem(new GUIContent($"Paste {name} Values"), pasteMotionValues, crudable.Clipboard == null || item.GetType() != crudable.Clipboard.GetType());
            void pasteMotionAsNew()
            {
                object copy = (T)Activator.CreateInstance(crudable.Clipboard.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(crudable.Clipboard, copy);
                target.PersistentInsertArrayElementAtIndex(target.arraySize, copy);
            }
            context.AddItem(new GUIContent($"Paste {name} as new"), pasteMotionAsNew, crudable.Clipboard == null);
        }

        internal static void ShowCrud<T>(GenericMenu context, SerializedProperty property, string name, ICrudable<T> crudable)
        {
            SerializedProperty target = property.Copy();
            object item = target.GetValue<object>();
            void copyMotion()
            {
                crudable.Clipboard = (T)Activator.CreateInstance(item.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(item, crudable.Clipboard);
            }
            context.AddItem(new GUIContent($"Copy {name}"), copyMotion);
            void pasteMotionValues()
            {
                object copy = (T)Activator.CreateInstance(crudable.Clipboard.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(crudable.Clipboard, copy);
                target.PersistentSetValue(copy);
            }
            context.AddItem(new GUIContent($"Paste {name} Values"), pasteMotionValues, crudable.Clipboard == null || item.GetType() != crudable.Clipboard.GetType());
        }

        internal static Rect GetRect(Rect position, int index)
            => GetRect(position, index, FlowEntConstants.SpacedSingleLineHeight, EditorGUIUtility.singleLineHeight);

        internal static Rect GetRect(Rect position, int index, float spacedLineHeight, float lineHeight)
            => new Rect(position.x, position.y + (index * spacedLineHeight), position.width, lineHeight);
    }
}
