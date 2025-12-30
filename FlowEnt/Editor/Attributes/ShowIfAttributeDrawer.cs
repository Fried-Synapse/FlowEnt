using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : AbstractIfAttributeDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => HasValue(property)
                ? EditorGUI.GetPropertyHeight(property, label, true)
                : 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!HasValue(property))
            {
                return;
            }
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property, label, true);
            EditorGUI.EndProperty();
        }
    }
}