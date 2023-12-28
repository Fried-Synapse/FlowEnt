using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EnableIfAttribute))]
    public class EnableIfAttributeDrawer : AbstractIfAttributeDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            bool guiState = GUI.enabled;
            bool fieldState = HasValue(property);

            GUI.enabled = fieldState;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = guiState;
        }
    }
}