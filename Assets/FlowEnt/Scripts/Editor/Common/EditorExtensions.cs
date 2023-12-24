using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEditorInternal;
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

        internal static string GetUniqueId(this SerializedProperty property)
        {
            return $"{property.serializedObject.targetObject.GetHashCode()}{property.propertyPath}";
        }

        internal static T GetValue<T>(this SerializedProperty property)
            => (T)GetValueAndFieldInfo(property).Item1;

        internal static FieldInfo GetFieldInfo(this SerializedProperty property)
            => GetValueAndFieldInfo(property).Item2;

        private static (object, FieldInfo) GetValueAndFieldInfo(this SerializedProperty property)
        {
            static object getValue(object source, string name, out FieldInfo fieldInfo)
            {
                fieldInfo = null;
                if (source == null)
                {
                    return null;
                }

                Type type = source.GetType();
                fieldInfo = type.GetField(name, ReflectionExtensions.DefaultBindingFlags);
                return fieldInfo?.GetValue(source);
            }

            static object getArrayValue(object source, string name, int index)
            {
                if (getValue(source, name, out _) is not IEnumerable enumerable)
                {
                    return null;
                }

                IEnumerator enumerator = enumerable.GetEnumerator();
                while (index-- >= 0)
                {
                    enumerator.MoveNext();
                }

                return enumerator.Current;
            }

            object obj = property.serializedObject.targetObject;
            FieldInfo fieldInfo = null;
            string path = property.propertyPath.Replace(".Array.data[", "[");
            string[] elementsNames = path.Split('.');
            foreach (string elementName in elementsNames.Take(elementsNames.Length))
            {
                if (elementName.Contains("["))
                {
                    string namePart = elementName.Substring(0, elementName.IndexOf("["));
                    int index = Convert.ToInt32(elementName.Substring(elementName.IndexOf("[")).Replace("[", "")
                        .Replace("]", ""));
                    obj = getArrayValue(obj, namePart, index);
                }
                else
                {
                    obj = getValue(obj, elementName, out fieldInfo);
                }
            }

            return (obj, fieldInfo);
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
            object parent = (parentProperty == null)
                ? property.serializedObject.targetObject
                : parentProperty.GetValue<object>();
            FieldInfo field = parent.GetType().GetField(property.name, ReflectionExtensions.DefaultBindingFlags);
            field.SetValue(parent, item);
            EditorUtility.SetDirty(property.serializedObject.targetObject);
            property.serializedObject.ApplyModifiedProperties();
        }

        internal static void PersistentAddArrayElement(this SerializedProperty listProperty, object item)
            => PersistentInsertArrayElementAtIndex(listProperty, listProperty.arraySize, item);

        internal static void PersistentInsertArrayElementAtIndex(this SerializedProperty listProperty, int index,
            object item)
            => PersistentSetArrayElementAtIndex(listProperty, index, item,
                () =>
                {
                    listProperty.InsertArrayElementAtIndex(index);
                    listProperty.serializedObject.ApplyModifiedProperties();
                });

        internal static void PersistentSetArrayElementAtIndex(this SerializedProperty listProperty, int index,
            object item, Action onSetting = null)
        {
            if (!listProperty.isArray)
            {
                throw new ArgumentException($"{nameof(listProperty)} is not an array.");
            }

            listProperty.serializedObject.Update();
            onSetting?.Invoke();
            SerializedProperty listItemProperty = listProperty.GetArrayElementAtIndex(index);
            if (listItemProperty.propertyType == SerializedPropertyType.ManagedReference)
            {
                listProperty.GetArrayElementAtIndex(index).managedReferenceValue = item;
            }
            else
            {
                listProperty.GetValue<IList>()[index] = item;
            }

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

        internal static void PersistentClearArray(this SerializedProperty listProperty)
        {
            if (!listProperty.isArray)
            {
                throw new ArgumentException($"{nameof(listProperty)} is not an array.");
            }

            listProperty.serializedObject.Update();
            listProperty.ClearArray();
            listProperty.serializedObject.ApplyModifiedProperties();
        }

        internal static void ExpandArray(this SerializedProperty listProperty, bool isExpanded)
        {
            if (!listProperty.isArray)
            {
                throw new ArgumentException($"{nameof(listProperty)} is not an array.");
            }

            listProperty.serializedObject.Update();
            for (int i = 0; i < listProperty.arraySize; i++)
            {
                listProperty.GetArrayElementAtIndex(i).isExpanded = isExpanded;
            }

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

        internal static void Add(this ReorderableList list, object item)
            => list.serializedProperty.PersistentAddArrayElement(item);

        internal static void AddItem(this GenericMenu menu, GUIContent content, GenericMenu.MenuFunction callback,
            bool isDisabled = false, bool isOn = false)
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