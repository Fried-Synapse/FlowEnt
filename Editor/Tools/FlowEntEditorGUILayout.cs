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
            EditorGUILayout.LabelField(name, headStyle, GUILayout.Height(fontSize + 20f));
        }

        internal static void LabelField(string text, string colour = FlowEntConstants.Grey)
        {
            EditorGUILayout.LabelField($"<color={colour}>{text}</color>", LabelStyle);
        }

        internal static void LabelFieldBold(string text, string colour = FlowEntConstants.Grey)
        {
            EditorGUILayout.LabelField($"<color={colour}><b>{text}</b></color>", LabelStyle);
        }

        internal static void LabelField(AbstractUpdatable updatable, string name)
        {
            EditorGUILayout.LabelField($"<color={FlowEntConstants.Grey}><b>{name}</b> {updatable}</color>", LabelStyle);
        }

        internal static void LabelField(IMotion motion)
        {
            EditorGUILayout.LabelField($"<color={FlowEntConstants.Grey}><b>{motion.GetType().Name}</b> - {motion.GetType().FullName}</color>", LabelStyle);
        }

        internal static Texture2D LoadTexture(string filePath)
        {
            filePath = Path.Combine(Application.dataPath, filePath);
            if (!File.Exists(filePath))
            {
                return null;
            }
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(1, 1);
            texture.LoadImage(fileData);
            return texture;
        }

        internal static T GetFieldValue<T>(this object obj, string name)
        {
            // Set the flags so that private and public fields from instances will be found
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            FieldInfo field = obj.GetType().GetField(name, bindingFlags);
            return (T)field?.GetValue(obj);
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
    }
}
