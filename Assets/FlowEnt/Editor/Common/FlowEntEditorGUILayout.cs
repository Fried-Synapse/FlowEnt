using System.IO;
using System.Reflection;
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

        internal static AbstractUpdatable GetUpdatableIndex(this object updateController)
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
            // Set the flags so that private and public fields from instances will be found
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo field = obj.GetType().GetField(name, bindingFlags);
            return (T)field?.GetValue(obj);
        }
    }
}
