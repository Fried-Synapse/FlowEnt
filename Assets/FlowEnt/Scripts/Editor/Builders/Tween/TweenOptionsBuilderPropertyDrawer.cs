using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenOptionsBuilder))]
    public class TweenOptionsBuilderPropertyDrawer : AbstractPropertiesBuilderPropertyDrawer<
        TweenOptionsBuilderPropertyDrawer.FieldsEnum>
    {
        public enum FieldsEnum
        {
            name,
            updateType,
            skipFrames,
            delay,
            timeScale,
            time,
            easingType,
            easing,
            loopCount,
            loopType
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
                            "isLoopCountInfinite", true, new GUIContent("Is Infinite"));
                        break;
                    case FieldsEnum.easing:
                        TweenOptionsBuilder.EasingType easingType =
                            (TweenOptionsBuilder.EasingType)property
                                .FindPropertyRelative(nameof(FieldsEnum.easingType)).enumValueIndex;
                        string propertyName = easingType switch
                        {
                            TweenOptionsBuilder.EasingType.Predefined => nameof(FieldsEnum.easing),
                            TweenOptionsBuilder.EasingType.AnimationCurve => "easingCurve",
                            _ => throw new System.NotImplementedException(),
                        };
                        EditorGUI.PropertyField(propertyPosition, property.FindPropertyRelative(propertyName));
                        break;
                    default:
                        EditorGUI.PropertyField(propertyPosition, property.FindPropertyRelative(prop.ToString()));
                        break;
                }
            }
        }
    }
}