using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(RandomBuilder<>), true)]
    public class RandomBuilderPropertyDrawer : PropertyDrawer
    {
        private enum PropertiesEnum
        {
            min,
            max,
        }

        private const int Padding = 10;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property.FindPropertyRelative(PropertiesEnum.min.ToString())) +
               EditorGUI.GetPropertyHeight(property.FindPropertyRelative(PropertiesEnum.max.ToString())) +
               FlowEntConstants.DrawerSpacing;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            float minMaxLabelWidth =
                EditorStyles.label.CalcSize(new GUIContent(property.FindPropertyRelative(PropertiesEnum.max.ToString()).displayName)).x + Padding;
            float labelWidth = EditorGUIUtility.labelWidth - minMaxLabelWidth;

            Rect labelPosition = position;
            labelPosition.width = labelWidth - Padding;
            EditorGUI.LabelField(labelPosition, label);

            position.x += EditorGUIUtility.labelWidth - minMaxLabelWidth;
            position.width -= EditorGUIUtility.labelWidth - minMaxLabelWidth;
            float previousLabelWidth = EditorGUIUtility.labelWidth;
            
            EditorGUIUtility.labelWidth = minMaxLabelWidth;
            FlowEntEditorGUILayout.PropertyField(ref position, property.FindPropertyRelative(PropertiesEnum.min.ToString()));
            FlowEntEditorGUILayout.PropertyField(ref position, property.FindPropertyRelative(PropertiesEnum.max.ToString()));
            EditorGUIUtility.labelWidth = previousLabelWidth;
            EditorGUI.EndProperty();
        }
    }
}