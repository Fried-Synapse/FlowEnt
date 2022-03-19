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

        public static AbstractUpdatable GetUpdatableIndex(this FlowEntController controller, string fieldName)
            => GetUpdatableIndexForObject(controller, fieldName);

        private static AbstractUpdatable GetUpdatableIndexForObject(object updateController, string fieldName = "updatables")
        {
            object updatables = updateController.GetFieldValue<object>(fieldName);
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
                FieldInfo fieldInfo = type.GetField(name, ReflectionExtensions.DefaultBindingFlags);
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

        public static SerializedProperty GetParent(this SerializedProperty property)
        {
            string path = property.propertyPath;
            int index = path.LastIndexOf('.');
            return index < 0 ? null : property.serializedObject.FindProperty(path.Substring(0, index));
        }

        public static SerializedProperty GetParentArray(this SerializedProperty property)
        {
            string path = property.propertyPath;
            int index = path.LastIndexOf('.') - 1;
            if (index < 0)
            {
                return null;
            }
            index = path.LastIndexOf('.', index);
            if (index < 0)
            {
                return null;
            }
            SerializedProperty parentProperty = property.serializedObject.FindProperty(path.Substring(0, index));
            return parentProperty.isArray ? parentProperty : null;
        }

        public static void PersistentSetValue(this SerializedProperty property, object item)
        {
            property.serializedObject.Update();
            SerializedProperty parentProperty = property.GetParent();
            object parent = (parentProperty == null) ? property.serializedObject.targetObject : parentProperty.GetValue<object>();
            FieldInfo field = parent.GetType().GetField(property.name, ReflectionExtensions.DefaultBindingFlags);
            field.SetValue(parent, item);
            EditorUtility.SetDirty(property.serializedObject.targetObject);
            property.serializedObject.ApplyModifiedProperties();
        }

        public static void PersistentInsertArrayElementAtIndex(this SerializedProperty listProperty, int index, object item)
        {
            if (!listProperty.isArray)
            {
                throw new ArgumentException($"{nameof(listProperty)} is not an array.");
            }
            listProperty.serializedObject.Update();
            listProperty.InsertArrayElementAtIndex(index);
            listProperty.GetArrayElementAtIndex(index).managedReferenceValue = item;
            listProperty.serializedObject.ApplyModifiedProperties();
        }

        public static void PersistentSetArrayElementAtIndex(this SerializedProperty listProperty, int index, object item)
        {
            if (!listProperty.isArray)
            {
                throw new ArgumentException($"{nameof(listProperty)} is not an array.");
            }
            listProperty.serializedObject.Update();
            listProperty.GetArrayElementAtIndex(index).managedReferenceValue = item;
            listProperty.serializedObject.ApplyModifiedProperties();
        }

        public static void PersistentDeleteArrayElementAtIndex(this SerializedProperty listProperty, int index)
        {
            if (!listProperty.isArray)
            {
                throw new ArgumentException($"{nameof(listProperty)} is not an array.");
            }
            listProperty.serializedObject.Update();
            listProperty.DeleteArrayElementAtIndex(index);
            listProperty.serializedObject.ApplyModifiedProperties();
        }

        public static int GetArrayElementIndex(this SerializedProperty listProperty, SerializedProperty item)
        {
            if (!listProperty.isArray)
            {
                throw new ArgumentException($"{nameof(listProperty)} is not an array.");
            }
            for (int i = 0; i < listProperty.arraySize; i++)
            {
                if (listProperty.GetArrayElementAtIndex(i).propertyPath == item.propertyPath)
                {
                    return i;
                }
            }
            return -1;
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
