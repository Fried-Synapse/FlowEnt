using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenEventsBuilder))]
    public class TweenEventsBuilderPropertyDrawer : AbstractPropertiesBuilderPropertyDrawer<TweenEventsBuilderPropertyDrawer.PropertiesEnum>
    {
        public enum PropertiesEnum
        {
            onStarting,
            onStarted,
            onUpdating,
            onUpdated,
            onLoopCompleted,
            onCompleting,
            onCompleted,
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => property.isExpanded ? FlowEntConstants.SpacedSingleLineHeight + (PropertyHeight * Properties.Count) : EditorGUIUtility.singleLineHeight;

        protected override float PropertyHeight => 100f;

        protected override void DrawProperties(Rect position, SerializedProperty property)
        {
            position.y += FlowEntConstants.SpacedSingleLineHeight;
            for (int i = 0; i < Properties.Count; i++)
            {
                PropertiesEnum prop = Properties[i];
                Rect propertyPosition = FlowEntEditorGUILayout.GetRect(position, i, PropertyHeight, PropertyHeight);
                EditorGUI.PropertyField(propertyPosition, property.FindPropertyRelative(prop.ToString()));
            }
        }
    }
}
