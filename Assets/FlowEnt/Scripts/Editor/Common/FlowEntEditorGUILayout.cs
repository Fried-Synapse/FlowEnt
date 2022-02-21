using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
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

        public static AbstractUpdatable GetUpdatableIndex(this Flow flow)
            => GetUpdatableIndexForObject(flow);

        public static AbstractUpdatable GetUpdatableIndex(this FlowEntController controller)
            => GetUpdatableIndexForObject(controller);

        private static AbstractUpdatable GetUpdatableIndexForObject(object updateController)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo field = updateController.GetType().GetField("updatables", bindingFlags);
            object updatables = field?.GetValue(updateController);

            field = updatables.GetType().GetField("anchor", bindingFlags);
            object anchor = field?.GetValue(updatables);

            field = anchor.GetType().GetField("next", bindingFlags);
            return (AbstractUpdatable)field?.GetValue(anchor);
        }

        internal static T GetFieldValue<T>(this object obj, string name)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo field = obj.GetType().GetField(name, bindingFlags);
            return (T)field?.GetValue(obj);
        }

        internal static void SetFieldValue<T>(this object obj, string name, T value)
        {
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo field = obj.GetType().GetField(name, bindingFlags);
            field?.SetValue(obj, value);
        }

        internal static T GetValue<T>(this SerializedProperty prop)
        {
            static object getValue(object source, string name)
            {
                if (source == null)
                {
                    return null;
                }
                Type type = source.GetType();
                FieldInfo fieldInfo = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                return fieldInfo.GetValue(source);
            }

            static object getArrayValue(object source, string name, int index)
            {
                IEnumerable enumerable = getValue(source, name) as IEnumerable;
                IEnumerator enumerator = enumerable.GetEnumerator();
                while (index-- >= 0)
                {
                    enumerator.MoveNext();
                }
                return enumerator.Current;
            }

            string path = prop.propertyPath.Replace(".Array.data[", "[");
            object obj = prop.serializedObject.targetObject;
            string[] elements = path.Split('.');
            foreach (string element in elements.Take(elements.Length))
            {
                if (element.Contains("["))
                {
                    string elementName = element.Substring(0, element.IndexOf("["));
                    int index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                    obj = getArrayValue(obj, elementName, index);
                }
                else
                {
                    obj = getValue(obj, element);
                }
            }
            return (T)obj;
        }

        public static void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
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

        internal static Rect GetRect(Rect position, int index)
            => GetRect(position, index, FlowEntConstants.SpacedSingleLineHeight, EditorGUIUtility.singleLineHeight);

        internal static Rect GetRect(Rect position, int index, float spacedLineHeight, float lineHeight)
            => new Rect(position.x, position.y + (index * spacedLineHeight), position.width, lineHeight);
    }
}
