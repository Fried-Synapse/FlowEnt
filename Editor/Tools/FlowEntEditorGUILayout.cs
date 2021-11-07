using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class FlowEntEditorGUILayout
    {
        internal static void Header(string name, int fontSize = 30)
        {
            GUIStyle headStyle = new GUIStyle(EditorStyles.largeLabel);
            headStyle.fontSize = fontSize;
            headStyle.fontStyle = FontStyle.Bold;
            headStyle.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.LabelField(name, headStyle, GUILayout.Height(fontSize + 20));
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
