using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class MotionNameGetter
    {
        private static readonly Regex nestedRegEx = new Regex("\\+");
        private static readonly Regex prettyNameRegEx = new Regex("[A-Z]");

        public static List<string> GetNames(Type type, IMotionBuilder motionBuilder = null)
        {
            List<string> names = new List<string>();
            if (motionBuilder != null && !string.IsNullOrEmpty(motionBuilder.DisplayName))
            {
                names.Add(motionBuilder.DisplayName);
            }

            DisplayNameAttribute displayNameAttribute = type.GetCustomAttribute<DisplayNameAttribute>();
            if (displayNameAttribute != null)
            {
                names.Add(displayNameAttribute.DisplayName);
            }

            static string getPrettyName(Type type)
            {
                const string builder = "Builder";
                const string flowEntAssembly = "FriedSynapse.FlowEnt";

                string[] nameParts = type.FullName.Split('.');
                string prettyName = nameParts[nameParts.Length - 1];
                prettyName = nestedRegEx.Replace(prettyName, " -");
                prettyName = prettyNameRegEx.Replace(prettyName, " $0");
                string[] prettyParts = prettyName.Split(' ');
                if (prettyParts[prettyParts.Length - 1] == builder)
                {
                    prettyName = Regex.Replace(prettyName, $"{(type.Name == builder ? $" - {builder}" : builder)}$",
                        string.Empty);
                }

                if (type.Assembly.GetName().Name == flowEntAssembly)
                {
                    prettyName = $"{type.Namespace.Split('.').Last()} -{prettyName}";
                }

                return prettyName.TrimStart();
            }

            names.Add(getPrettyName(type));

            static string getName(Type type)
            {
                string[] nameParts = type.FullName.Split('.');
                string name = nameParts[nameParts.Length - 1];
                return nestedRegEx.Replace(name, "-");
            }

            names.Add(getName(type));
            return names;
        }
    }

    [CustomPropertyDrawer(typeof(IMotionBuilder), true)]
    public class AbstractMotionBuilderPropertyDrawer : PropertyDrawer, ICrudable<IMotionBuilder>
    {
        private readonly List<string> hiddenProperties = new List<string>
        {
            IdentifiableBuilderFields.DisplayName,
            IdentifiableBuilderFields.IsDisplayNameEnabled,
            IdentifiableBuilderFields.IsEnabled,
        };

        private static IMotionBuilder clipboard;

        public IMotionBuilder Clipboard
        {
            get => clipboard;
            set => clipboard = value;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight + 5;
            height += IdentifiableBuilderFields.GetDisplayNameHeight(property);

            ForEachVisibleProperty(property, p => height += EditorGUI.GetPropertyHeight(p, true));
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect headerPosition = FlowEntEditorGUILayout.GetRect(position, 0);
            IMotionBuilder motionBuilder = property.GetValue<IMotionBuilder>();
            string name = MotionNameGetter.GetNames(motionBuilder.GetType(), motionBuilder)[0];
            label.text = name.PadLeft(name.Length + 6);
            property.isExpanded = EditorGUI.Foldout(headerPosition, property.isExpanded, label);

            Rect isEnabledPosition = headerPosition;
            isEnabledPosition.x += 2f;
            isEnabledPosition.width = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(isEnabledPosition,
                property.FindPropertyRelative(IdentifiableBuilderFields.IsEnabled), GUIContent.none);

            DrawMenu(headerPosition, property);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            position.y += FlowEntConstants.SpacedSingleLineHeight;
            IdentifiableBuilderFields.DrawDisplayName(ref position, property);

            ForEachVisibleProperty(property, p =>
            {
                float height = EditorGUI.GetPropertyHeight(p, true) + FlowEntConstants.DrawerSpacing;
                position.height = height;
                EditorGUI.PropertyField(position, p, true);
                position.y += height;
            });
            EditorGUI.indentLevel--;
        }

        private void DrawMenu(Rect position, SerializedProperty property)
        {
            Rect menuPosition = position;
            const float menuWidth = 20f;
            menuPosition.x = position.xMax - (menuWidth / 2f) - 10;
            menuPosition.width = menuWidth;
            menuPosition.height = EditorGUIUtility.singleLineHeight;
            if (GUI.Button(menuPosition, Icon.Menu, Icon.Style))
            {
                SerializedProperty parentProperty = property.GetParentArray();
                GenericMenu context = new GenericMenu();
                FlowEntEditorGUILayout.ShowListCrud(context, parentProperty,
                    parentProperty.GetArrayElementIndex(property), "Motion", this);
                context.AddSeparator(string.Empty);
                IdentifiableBuilderFields.DrawShowRename(property, context);
                context.ShowAsContext();
            }
        }

        private void ForEachVisibleProperty(SerializedProperty property, Action<SerializedProperty> predicate)
        {
            FlowEntEditorGUILayout.ForEachVisibleProperty(property, p =>
            {
                if (!hiddenProperties.Contains(p.name))
                {
                    predicate(p);
                }
            });
        }
    }
}