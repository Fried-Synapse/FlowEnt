using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    //TODO
    //[CustomPropertyDrawer(typeof(TweenOptions))]
    public class TweenOptionsDrawer : PropertyDrawer
    {
        private const int lineHeight = 18;
        private SerializedProperty autoStartField;
        private SerializedProperty skipFramesField;
        private SerializedProperty delayField;
        private SerializedProperty timeField;
        private SerializedProperty timeScaleField;
        private SerializedProperty easingField;
        private int? loopCountField;
        private SerializedProperty loopTypeField;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 8 * lineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ReadField(property);

            DrawFields(position);
        }

        private void DrawFields(Rect position)
        {
            EditorGUI.PropertyField(position, autoStartField);
            position.y += lineHeight;
            EditorGUI.PropertyField(position, skipFramesField);
            position.y += lineHeight;
            EditorGUI.PropertyField(position, delayField);
            position.y += lineHeight;
            EditorGUI.PropertyField(position, timeField);
            position.y += lineHeight;
            EditorGUI.PropertyField(position, timeScaleField);
            // position.y += lineHeight;
            // EditorGUI.PropertyField(position, easingField);
            // position.y += lineHeight;
            // EditorGUI.PropertyField(position, loopCountField);
            position.y += lineHeight;
            EditorGUI.PropertyField(position, loopTypeField);
        }

        private void ReadField(SerializedProperty property)
        {
            autoStartField = property.FindPropertyRelative("autoStart");
            skipFramesField = property.FindPropertyRelative("skipFrames");
            delayField = property.FindPropertyRelative("delay");
            timeField = property.FindPropertyRelative("time");
            timeScaleField = property.FindPropertyRelative("timeScale");
            easingField = property.FindPropertyRelative("easing");
            loopCountField = null;
            loopTypeField = property.FindPropertyRelative("loopType");
        }
    }
}
