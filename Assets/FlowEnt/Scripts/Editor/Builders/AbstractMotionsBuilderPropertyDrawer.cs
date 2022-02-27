using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenMotionsBuilder))]
    public abstract class AbstractMotionsBuilderPropertyDrawer<TMotionBuilder> : PropertyDrawer
        where TMotionBuilder : IMotionBuilder
    {
        protected static class Icon
        {
            public static GUIContent Menu = EditorGUIUtility.IconContent("_Menu@2x", "Menu");
            public static GUIStyle Style = new GUIStyle(EditorStyles.miniButton) { padding = new RectOffset(2, 2, 2, 2) };
        }

        private readonly Queue<TMotionBuilder> addedBuildersTypes = new Queue<TMotionBuilder>();
        private static TMotionBuilder clipboard;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded)
            {
                return EditorGUIUtility.singleLineHeight;
            }

            float height = FlowEntConstants.SpacedSingleLineHeight * 2;
            SerializedProperty motionsProperty = property.FindPropertyRelative("motions");
            for (int i = 0; i < motionsProperty.arraySize; i++)
            {
                SerializedProperty motionProperty = motionsProperty.GetArrayElementAtIndex(i);
                height += EditorGUI.GetPropertyHeight(motionProperty, true);
            }
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded, label, EditorStyles.foldoutHeader);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            Rect buttonPosition = FlowEntEditorGUILayout.GetRect(position, 1);
            if (GUI.Button(buttonPosition, "Add motion"))
            {
                MotionPickerWindow.Show<TMotionBuilder>(AddMotion);
            }
            DrawMotions(FlowEntEditorGUILayout.GetRect(position, 2), property);
            EditorGUI.indentLevel--;
        }

        private void DrawMotions(Rect position, SerializedProperty property)
        {
            GameObject gameObject = (property.serializedObject.targetObject as MonoBehaviour)?.gameObject;
            if (gameObject == null)
            {
                return;
            }
            SerializedProperty motionsProperty = property.FindPropertyRelative("motions");
            List<TMotionBuilder> motions = motionsProperty.GetValue<List<TMotionBuilder>>();

            while (addedBuildersTypes.Count > 0)
            {
                TMotionBuilder builder = addedBuildersTypes.Dequeue();
                motions.Add(builder);
            }

            for (int i = 0; i < motionsProperty.arraySize; i++)
            {
                DrawMenu(position, motions, motions[i]);
                SerializedProperty motionProperty = motionsProperty.GetArrayElementAtIndex(i);
                float height = EditorGUI.GetPropertyHeight(motionProperty, true) + FlowEntConstants.DrawerSpacing;
                position.height = height;
                EditorGUI.PropertyField(position, motionProperty, true);
                position.y += height;
            }
        }

        private void AddMotion(TMotionBuilder builder)
        {
            addedBuildersTypes.Enqueue(builder);
        }

        private void DrawMenu(Rect position, List<TMotionBuilder> motions, TMotionBuilder motion)
        {
            Rect menuPosition = position;
            const float menuWidth = 20f;
            menuPosition.x = position.xMax - (menuWidth / 2f) - 10;
            menuPosition.width = menuWidth;
            menuPosition.height = EditorGUIUtility.singleLineHeight;
            //NOTE this should not be in here...it should be in the motion builder drawer
            if (GUI.Button(menuPosition, Icon.Menu, Icon.Style))
            {
                GenericMenu context = new GenericMenu();
                context.AddItem(new GUIContent("Remove Motion"), () => motions.Remove(motion));
                void copyMotion()
                {
                    clipboard = (TMotionBuilder)Activator.CreateInstance(motion.GetType());
                    EditorUtility.CopySerializedManagedFieldsOnly(motion, clipboard);
                }
                context.AddItem(new GUIContent("Copy Motion"), copyMotion);
                context.AddItem(new GUIContent("Paste Motion Values"), () => EditorUtility.CopySerializedManagedFieldsOnly(clipboard, motion), clipboard == null || motion.GetType() != clipboard.GetType());
                context.AddItem(new GUIContent("Paste Motion as new"), () => motions.Add(clipboard), clipboard == null);
                context.ShowAsContext();
            }
        }
    }
}
