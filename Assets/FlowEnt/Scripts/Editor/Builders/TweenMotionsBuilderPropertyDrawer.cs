using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UEditor = UnityEditor.Editor;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenMotionsBuilder))]
    public class TweenMotionsBuilderPropertyDrawer : PropertyDrawer
    {
        private Queue<Type> AddedBuildersTypes { get; } = new Queue<Type>();

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => 300;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(FlowEntEditorGUILayout.GetRect(position, 0), property.isExpanded, label);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;
            Rect buttonPosition = FlowEntEditorGUILayout.GetRect(position, 1);
            if (GUI.Button(buttonPosition, "Add motion"))
            {
                MotionPickerWindow.Show(AddMotion);
            }
            DrawMotions(FlowEntEditorGUILayout.GetRect(position, 1), property);
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

            while (AddedBuildersTypes.Count > 0)
            {
                Type type = AddedBuildersTypes.Dequeue();
                AbstractTweenMotionBuilder builder = (AbstractTweenMotionBuilder)gameObject.AddComponent(type);
                motionsProperty.InsertArrayElementAtIndex(motionsProperty.arraySize);
                motionsProperty.GetArrayElementAtIndex(motionsProperty.arraySize - 1).objectReferenceValue = builder;
            }

            for (int i = 0; i < motionsProperty.arraySize; i++)
            {
                UEditor editor = UEditor.CreateEditor(motionsProperty.GetArrayElementAtIndex(i).objectReferenceValue);
                SerializedObject motionSerialisedObject = editor.serializedObject;
                SerializedProperty motionProperty = motionSerialisedObject.GetIterator();
                motionProperty.NextVisible(true);

                int indent = EditorGUI.indentLevel;
                int depthChildren = 0;
                bool showChildren = false;
                while (motionProperty.NextVisible(true))
                {
                    if (motionProperty.depth == 0)
                    {
                        showChildren = false;
                        depthChildren = 0;
                    }
                    if (showChildren && motionProperty.depth > depthChildren)
                    {
                        continue;
                    }
                    position = FlowEntEditorGUILayout.GetRect(position, 1);
                    EditorGUI.indentLevel = indent + motionProperty.depth;
                    if (EditorGUI.PropertyField(position, motionProperty))
                    {
                        showChildren = false;
                    }
                    else
                    {
                        showChildren = true;
                        depthChildren = motionProperty.depth;
                    }
                }

                if (GUI.changed)
                {
                    motionSerialisedObject.ApplyModifiedProperties();
                }
            }
        }

        private void AddMotion(Type builder)
        {
            AddedBuildersTypes.Enqueue(builder);
        }
    }
}
