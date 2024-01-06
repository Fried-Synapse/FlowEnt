using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(UrlButtonAttribute))]
    public class UrlButtonAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property, label, true);

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, property, label);

            UrlButtonAttribute urlButtonAttribute = (UrlButtonAttribute)attribute;
            position.width = position.height;
            position.x += EditorGUIUtility.labelWidth - position.width;
            GUIContent icon = new(Icon.Info)
            {
                tooltip = urlButtonAttribute.Tooltip
            };
            if (GUI.Button(position, icon, Icon.Style))
            {
                Application.OpenURL(urlButtonAttribute.Url);
            }
            EditorGUI.EndProperty();
        }
    }
}