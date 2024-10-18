using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(FlowOptionsBuilder))]
    public class FlowOptionsBuilderPropertyDrawer : AbstractPropertiesBuilderPropertyDrawer<
        FlowOptionsBuilderPropertyDrawer.FieldsEnum>
    {
        public enum FieldsEnum
        {
            name,
            updateType,
            skipFrames,
            delay,
            timeScale,
            loopCount
        }

        protected override void DrawProperties(Rect position, SerializedProperty property)
        {
            for (int i = 0; i < Properties.Count; i++)
            {
                FieldsEnum prop = Properties[i];
                Rect propertyPosition = FlowEntEditorGUILayout.GetRectForLine(position, i + 1);
                switch (prop)
                {
                    case FieldsEnum.loopCount:
                        DrawNullable(propertyPosition, property, nameof(FieldsEnum.loopCount),
                            "isLoopCountInfinite", true);
                        break;
                    default:
                        EditorGUI.PropertyField(propertyPosition, property.FindPropertyRelative(prop.ToString()));
                        break;
                }
            }
        }
    }
}