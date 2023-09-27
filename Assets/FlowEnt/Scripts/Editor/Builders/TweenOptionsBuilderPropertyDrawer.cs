using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenOptionsBuilder))]
    public class TweenOptionsBuilderPropertyDrawer : AbstractPropertiesBuilderPropertyDrawer<
        TweenOptionsBuilderPropertyDrawer.PropertiesEnum>
    {
        public enum PropertiesEnum
        {
            name,
            updateType,
            autoStart,
            skipFrames,
            delay,
            timeScale,
            time,
            easingType,
            easing,
            loopCount,
            loopType
        }

        protected override float PropertyHeight => EditorGUIUtility.singleLineHeight;

        protected override void DrawProperties(Rect position, SerializedProperty property)
        {
            for (int i = 0; i < Properties.Count; i++)
            {
                PropertiesEnum prop = Properties[i];
                Rect propertyPosition = FlowEntEditorGUILayout.GetRect(position, i + 1);
                switch (prop)
                {
                    case PropertiesEnum.loopCount:
                        DrawNullable(propertyPosition, property, nameof(PropertiesEnum.loopCount),
                            "isLoopCountInfinite", true);
                        break;
                    case PropertiesEnum.easing:
                        TweenOptionsBuilder.EasingType easingType =
                            (TweenOptionsBuilder.EasingType)property
                                .FindPropertyRelative(nameof(PropertiesEnum.easingType)).enumValueIndex;
                        string propertyName = easingType switch
                        {
                            TweenOptionsBuilder.EasingType.Predefined => nameof(PropertiesEnum.easing),
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