using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class EditorExtensions
    {
        internal static AbstractUpdatable GetUpdatableIndex(this IUpdateController updateController, string fieldName)
        {
            object updatables = updateController.GetFieldValue<object>(fieldName);
            object anchor = updatables.GetFieldValue<object>(nameof(anchor));
            return anchor.GetFieldValue<AbstractUpdatable>("next");
        }

        internal static string ToClassName(this Enum @enum)
        {
            string typeName = @enum.ToString();
            return char.ToLower(typeName[0]) + typeName.Substring(1);
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

        internal static SerializedProperty GetParent(this SerializedProperty property)
        {
            string path = property.propertyPath;
            int index = path.LastIndexOf('.');
            return index < 0 ? null : property.serializedObject.FindProperty(path.Substring(0, index));
        }

        internal static SerializedProperty GetParentArray(this SerializedProperty property)
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

        internal static void PersistentSetValue(this SerializedProperty property, object item)
        {
            property.serializedObject.Update();
            SerializedProperty parentProperty = property.GetParent();
            object parent = (parentProperty == null) ? property.serializedObject.targetObject : parentProperty.GetValue<object>();
            FieldInfo field = parent.GetType().GetField(property.name, ReflectionExtensions.DefaultBindingFlags);
            field.SetValue(parent, item);
            EditorUtility.SetDirty(property.serializedObject.targetObject);
            property.serializedObject.ApplyModifiedProperties();
        }

        internal static void PersistentInsertArrayElementAtIndex(this SerializedProperty listProperty, int index, object item)
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

        internal static void PersistentSetArrayElementAtIndex(this SerializedProperty listProperty, int index, object item)
        {
            if (!listProperty.isArray)
            {
                throw new ArgumentException($"{nameof(listProperty)} is not an array.");
            }
            listProperty.serializedObject.Update();
            listProperty.GetArrayElementAtIndex(index).managedReferenceValue = item;
            listProperty.serializedObject.ApplyModifiedProperties();
        }

        internal static void PersistentDeleteArrayElementAtIndex(this SerializedProperty listProperty, int index)
        {
            if (!listProperty.isArray)
            {
                throw new ArgumentException($"{nameof(listProperty)} is not an array.");
            }
            listProperty.serializedObject.Update();
            listProperty.DeleteArrayElementAtIndex(index);
            listProperty.serializedObject.ApplyModifiedProperties();
        }

        internal static int GetArrayElementIndex(this SerializedProperty listProperty, SerializedProperty item)
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

        internal static void AddItem(this GenericMenu menu, GUIContent content, GenericMenu.MenuFunction callback, bool isDisabled = false, bool isOn = false)
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
