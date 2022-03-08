using System;
using System.Collections;
using FriedSynapse.FlowEnt.Motions.Abstract;
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

        private static GUIStyle foldoutStyle;
        internal static GUIStyle FoldoutStyle
        {
            get
            {
                if (foldoutStyle == null)
                {
                    foldoutStyle = new GUIStyle(EditorStyles.label)
                    {
                        richText = true,
                    };
                }
                return foldoutStyle;
            }
        }

        internal static void Header(string name, int fontSize = 30)
        {
            GUIStyle headStyle = new GUIStyle(EditorStyles.largeLabel);
            headStyle.fontSize = fontSize;
            headStyle.fontStyle = FontStyle.Bold;
            headStyle.alignment = TextAnchor.MiddleCenter;
            GUIContent content = new GUIContent(name, Resources.Load<Texture2D>("Logo"));
            EditorGUILayout.LabelField(content, headStyle, GUILayout.Height(fontSize + 20f));
        }

        internal static void LabelField(string text, string colour = FlowEntConstants.Grey, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField($"<color={colour}>{text}</color>", LabelStyle, options);
        }

        internal static void LabelFieldBold(string text, string colour = FlowEntConstants.Grey, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField($"<color={colour}><b>{text}</b></color>", LabelStyle, options);
        }

        internal static void LabelField(AbstractUpdatable updatable, string name, params GUILayoutOption[] options)
        {
            EditorGUILayout.LabelField($"<color={FlowEntConstants.Grey}><b>{name}</b> {updatable}</color>", LabelStyle, options);
        }

        internal static void LabelField(IMotion motion, params GUILayoutOption[] options)
        {
            GUIContent content = new GUIContent($"<color={FlowEntConstants.Grey}><b>{motion.GetType().Name}</b></color>", motion.GetType().FullName);
            EditorGUILayout.LabelField(content, LabelStyle, options);
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

        internal static void ShowCrud<T>(GenericMenu context, SerializedProperty list, int index, string name, ICrudable<T> copyPastable)
        {
            object item = list.GetArrayElementAtIndex(index).GetValue<object>();
            if (list != null)
            {
                context.AddItem(new GUIContent($"Remove {name}"), () => list.PersistentDeleteArrayElementAtIndex(index));
            }
            void copyMotion()
            {
                copyPastable.Clipboard = (T)Activator.CreateInstance(item.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(item, copyPastable.Clipboard);
            }
            context.AddItem(new GUIContent($"Copy {name}"), copyMotion);
            void pasteMotionValues()
            {
                object copy = (T)Activator.CreateInstance(copyPastable.Clipboard.GetType());
                EditorUtility.CopySerializedManagedFieldsOnly(copyPastable.Clipboard, copy);
                list.PersistentSetArrayElementAtIndex(index, copy);
            }
            context.AddItem(new GUIContent($"Paste {name} Values"), pasteMotionValues, copyPastable.Clipboard == null || item.GetType() != copyPastable.Clipboard.GetType());
            if (list != null)
            {
                void pasteMotionAsNew()
                {
                    object copy = (T)Activator.CreateInstance(copyPastable.Clipboard.GetType());
                    EditorUtility.CopySerializedManagedFieldsOnly(copyPastable.Clipboard, copy);
                    list.PersistentInsertArrayElementAtIndex(list.arraySize, copy);
                }
                context.AddItem(new GUIContent($"Paste {name} as new"), pasteMotionAsNew, copyPastable.Clipboard == null);
            }
        }

        internal static Rect GetIndentedRect(Rect position, float indent)
        {
            position.width -= indent;
            position.x += indent;
            return position;
        }

        internal static Rect GetRect(Rect position, int index)
            => GetRect(position, index, FlowEntConstants.SpacedSingleLineHeight, EditorGUIUtility.singleLineHeight);

        internal static Rect GetRect(Rect position, int index, float spacedLineHeight, float lineHeight)
            => new Rect(position.x, position.y + (index * spacedLineHeight), position.width, lineHeight);
    }
}
