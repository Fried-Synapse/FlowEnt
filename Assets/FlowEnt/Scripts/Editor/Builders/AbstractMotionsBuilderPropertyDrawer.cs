using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenMotionsBuilder))]
    public abstract class AbstractMotionsBuilderPropertyDrawer<T> : PropertyDrawer
        where T : class
    {
        private readonly Queue<T> addedBuildersTypes = new Queue<T>();

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
                MotionPickerWindow.Show<T>(AddMotion);
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
            List<T> motions = motionsProperty.GetValue<List<T>>();

            while (addedBuildersTypes.Count > 0)
            {
                T builder = addedBuildersTypes.Dequeue();
                motions.Add(builder);
            }

            for (int i = 0; i < motionsProperty.arraySize; i++)
            {
                Rect buttonPosition = position;
                const float width = 20f;
                buttonPosition.x = position.xMax - (width / 2f) - 10;
                buttonPosition.width = width;
                buttonPosition.height = EditorGUIUtility.singleLineHeight;
                if (GUI.Button(buttonPosition, "-"))
                {
                    motionsProperty.DeleteArrayElementAtIndex(i);
                    i--;
                    continue;
                }
                SerializedProperty motionProperty = motionsProperty.GetArrayElementAtIndex(i);
                float height = EditorGUI.GetPropertyHeight(motionProperty, true) + FlowEntConstants.DrawerSpacing;
                position.height = height;
                EditorGUI.PropertyField(position, motionProperty, true);
                position.y += height;
            }
        }

        private void AddMotion(T builder)
        {
            addedBuildersTypes.Enqueue(builder);
        }
    }
}
