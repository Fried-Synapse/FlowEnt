using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class EditorExtensions
    {
        public static AbstractUpdatable GetUpdatableIndex(this Flow flow)
            => GetUpdatableIndexForObject(flow);

        public static AbstractUpdatable GetUpdatableIndex(this FlowEntController controller)
            => GetUpdatableIndexForObject(controller);

        private static AbstractUpdatable GetUpdatableIndexForObject(object updateController)
        {
            object updatables = updateController.GetFieldValue<object>("updatables");
            object anchor = updatables.GetFieldValue<object>("anchor");
            return anchor.GetFieldValue<AbstractUpdatable>("next");
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

        public static void AddItem(this GenericMenu menu, GUIContent content, GenericMenu.MenuFunction callback, bool isDisabled = false, bool isOn = false)
        {
            if (isDisabled)
            {
                menu.AddDisabledItem(content, isOn);
            }
            else
            {
                menu.AddItem(content, isOn, callback);
            }
        }
    }
}
