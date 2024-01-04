using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(DisplayName), true)]
    public class DisplayNamePropertyDrawer : PropertyDrawer
    {
        private enum FieldsEnum
        {
            name,
            isVisible,
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => property.FindPropertyRelative(FieldsEnum.isVisible.ToString()).boolValue
                ? FlowEntConstants.SpacedSingleLineHeight
                : 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!property.FindPropertyRelative(FieldsEnum.isVisible.ToString()).boolValue)
            {
                return;
            }

            FlowEntEditorGUILayout.PropertyField(ref position, property.FindPropertyRelative(FieldsEnum.name.ToString()));
        }
    }
}