using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(MotionsBuilder))]
    public class MotionsBuilderPropertyDrawer : PropertyDrawer
    {
        private const string MotionBuildersName = "motionBuilders";
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded)
            {
                SerializedProperty motionBuildersProperty = property.FindPropertyRelative(MotionBuildersName);
                float motionsHeight = 0f;
                for (int i = 0; i < motionBuildersProperty.arraySize; i++)
                {
                    SerializedProperty motionBuilder = motionBuildersProperty.GetArrayElementAtIndex(i);
                    motionsHeight += EditorGUI.GetPropertyHeight(motionBuilder, true);
                }
                return (FlowEntConstants.SpacedSingleLineHeight * 2) + motionsHeight;
            }
            else
            {
                return FlowEntConstants.SingleLineHeight;
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.isExpanded = EditorGUI.Foldout(FlowEntDrawers.GetRect(position, out float y), property.isExpanded, label);

            if (!property.isExpanded)
            {
                return;
            }

            EditorGUI.indentLevel++;

            MotionsBuilder motionsBuilder = property.GetValue<MotionsBuilder>();
            motionsBuilder.MotionBuilders.ForEach(b => Debug.Log(b.GetType().Name));
            SerializedProperty motionBuildersProperty = property.FindPropertyRelative(MotionBuildersName);
            DrawMotions(FlowEntDrawers.GetRect(position, y, out _), motionBuildersProperty, out y);
            if (GUI.Button(FlowEntDrawers.GetRect(position, y + FlowEntConstants.DrawerSpacing, out y), "Select"))
            {
                MotionSelectionWindow.Open((builder) => motionsBuilder.MotionBuilders.Add(builder));
            }

            EditorGUI.indentLevel--;
        }

        private void DrawMotions(Rect position, SerializedProperty motionBuildersProperty, out float yMax)
        {
            yMax = FlowEntDrawers.GetRect(position, out _).y;
            position.width -= 20;
            for (int i = 0; i < motionBuildersProperty.arraySize; i++)
            {
                SerializedProperty motionBuilderProperty = motionBuildersProperty.GetArrayElementAtIndex(i);
                AbstractMotionBuilder motionBuilder = motionBuilderProperty.GetValue<AbstractMotionBuilder>();
                position = FlowEntDrawers.GetRect(position, yMax, motionBuilderProperty, out yMax);
                EditorGUI.PropertyField(position, motionBuilderProperty, true);

                Rect deleteButtonPosition = new Rect(position.x + position.width, position.y, 20, 20);
                if (GUI.Button(deleteButtonPosition, "-"))
                {
                    motionBuildersProperty.DeleteArrayElementAtIndex(i);
                    i--;
                }
            }
        }
    }
}
