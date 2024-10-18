using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoOptionsBuilder))]
    public class EchoOptionsBuilderPropertyDrawer : AbstractPropertiesBuilderPropertyDrawer<
        EchoOptionsBuilderPropertyDrawer.FieldsEnum>
    {
        public enum FieldsEnum
        {
            name,
            updateType,
            skipFrames,
            delay,
            timeScale,
            timeout,
            loopCount,
        }

        protected override void DrawProperties(Rect position, SerializedProperty property)
        {
            for (int i = 0; i < Properties.Count; i++)
            {
                FieldsEnum prop = Properties[i];
                Rect propertyPosition = FlowEntEditorGUILayout.GetRectForLine(position, i + 1);
                switch (prop)
                {
                    case FieldsEnum.timeout:
                        DrawNullable(propertyPosition, property, nameof(FieldsEnum.timeout), "hasTimeout");
                        break;
                    case FieldsEnum.loopCount:
                        DrawNullable(propertyPosition, property, nameof(FieldsEnum.loopCount),
                            "isLoopCountInfinite", true, new GUIContent("Is Infinite"));
                        break;
                    default:
                        EditorGUI.PropertyField(propertyPosition, property.FindPropertyRelative(prop.ToString()));
                        break;
                }
            }
        }
    }
}