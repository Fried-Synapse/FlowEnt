using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(IMotionBuilder), true)]
    public class AbstractMotionBuilderPropertyDrawer : PropertyDrawer
    {
        protected static class Icon
        {
            public static GUIContent Menu = EditorGUIUtility.IconContent("_Menu@2x", "Menu");
            public static GUIStyle Style = new GUIStyle(EditorStyles.miniButton) { padding = new RectOffset(2, 2, 2, 2) };
        }

        private static class ControlFields
        {
            public const string Name = "name";
            public const string IsNameEnabled = "isNameEnabled";
            public const string IsEnabled = "isEnabled";
        }

        private readonly List<string> hiddenProperties = new List<string>
        {
            ControlFields.Name,
            ControlFields.IsNameEnabled,
            ControlFields.IsEnabled,
        };
        private static IMotionBuilder clipboard;
        private static readonly Regex nestedRegEx = new Regex("\\+");
        private static readonly Regex prettyNameRegEx = new Regex("[A-Z]");

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight + 5;
            if (property.FindPropertyRelative(ControlFields.IsNameEnabled).boolValue)
            {
                height += FlowEntConstants.SpacedSingleLineHeight;
            }
            ForEachVisibleProperty(property, p => height += EditorGUI.GetPropertyHeight(p, true));
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Rect headerPosition = FlowEntEditorGUILayout.GetRect(position, 0);
            IMotionBuilder motionBuilder = property.GetValue<IMotionBuilder>();
            label.text = $"        {GetNames(motionBuilder.GetType(), motionBuilder)[0]}";
            property.isExpanded = EditorGUI.Foldout(headerPosition, property.isExpanded, label);

            Rect isEnabledPosition = headerPosition;
            isEnabledPosition.x = EditorGUIUtility.singleLineHeight * 1.2f;
            isEnabledPosition.width = EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(isEnabledPosition, property.FindPropertyRelative(ControlFields.IsEnabled), GUIContent.none);

            DrawMenu(headerPosition, property);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            position.y += FlowEntConstants.SpacedSingleLineHeight;
            var x = property.FindPropertyRelative(ControlFields.IsNameEnabled);
            if (property.FindPropertyRelative(ControlFields.IsNameEnabled).boolValue)
            {
                position.height = EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative(ControlFields.Name));
                position.y += FlowEntConstants.SpacedSingleLineHeight;
            }
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
                SerializedProperty motionsProperty = property.GetParentArray();
                IList motions = motionsProperty.GetValue<IList>();
                IMotionBuilder motion = property.GetValue<IMotionBuilder>();

                GenericMenu context = new GenericMenu();
                context.AddItem(new GUIContent("Remove Motion"), () => motions.Remove(motion));
                void copyMotion()
                {
                    clipboard = (IMotionBuilder)Activator.CreateInstance(motion.GetType());
                    EditorUtility.CopySerializedManagedFieldsOnly(motion, clipboard);
                }
                context.AddItem(new GUIContent("Copy Motion"), copyMotion);
                context.AddItem(new GUIContent("Paste Motion Values"), () => EditorUtility.CopySerializedManagedFieldsOnly(clipboard, motion), clipboard == null || motion.GetType() != clipboard.GetType());
                context.AddItem(new GUIContent("Paste Motion as new"), () => motions.Add(clipboard), clipboard == null);
                context.AddSeparator(string.Empty);
                SerializedProperty isNameEnabledProperty = property.FindPropertyRelative(ControlFields.IsNameEnabled);
                void showRename()
                {
                    isNameEnabledProperty.boolValue = !isNameEnabledProperty.boolValue;
                    isNameEnabledProperty.serializedObject.ApplyModifiedProperties();
                }
                context.AddItem(new GUIContent("Show Rename"), showRename, false, isNameEnabledProperty.boolValue);
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

        public static List<string> GetNames(Type type, IMotionBuilder motionBuilder = null)
        {
            List<string> names = new List<string>();
            if (motionBuilder != null && !string.IsNullOrEmpty(motionBuilder.Name))
            {
                names.Add(motionBuilder.Name);
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
                    prettyName = Regex.Replace(prettyName, $"{(type.Name == builder ? $" - {builder}" : builder)}$", string.Empty);
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
}
