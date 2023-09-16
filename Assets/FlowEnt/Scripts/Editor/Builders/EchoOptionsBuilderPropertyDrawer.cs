using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoOptionsBuilder))]
    public class EchoOptionsBuilderPropertyDrawer : AbstractPropertiesBuilderPropertyDrawer<
        EchoOptionsBuilderPropertyDrawer.PropertiesEnum>
    {
        public enum PropertiesEnum
        {
            name,
            updateType,
            autoStart,
            skipFrames,
            delay,
            timeScale,
            timeout,
            loopCount,
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
                    case PropertiesEnum.timeout:
                        DrawNullable(propertyPosition, property, nameof(PropertiesEnum.timeout), "hasTimeout");
                        break;
                    case PropertiesEnum.loopCount:
                        DrawNullable(propertyPosition, property, nameof(PropertiesEnum.loopCount),
                            "isLoopCountInfinite", true);
                        break;
                    default:
                        EditorGUI.PropertyField(propertyPosition, property.FindPropertyRelative(prop.ToString()));
                        break;
                }
            }
        }

        protected override void Init(SerializedProperty property)
        {
            base.Init(property);
            property.FindPropertyRelative(PropertiesEnum.timeScale.ToString()).floatValue =
                AbstractAnimationOptions.DefaultTimeScale;
            property.FindPropertyRelative(PropertiesEnum.loopCount.ToString()).intValue =
                AbstractAnimationOptions.DefaultLoopCount;
        }
    }
}