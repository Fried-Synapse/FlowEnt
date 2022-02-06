using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenOptionsBuilder))]
    public class TweenOptionsBuilderPropertyDrawer : AbstractBuilderPropertyDrawer<TweenOptionsBuilderPropertyDrawer.PropertiesEnum>
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
                Rect propertyPosition = FlowEntDrawers.GetRect(position, i + 1);
                switch (prop)
                {
                    case PropertiesEnum.loopCount:
                        DrawNullable(propertyPosition, property, nameof(PropertiesEnum.loopCount), "isLoopCountInfinite", true);
                        break;
                    default:
                        EditorGUI.PropertyField(propertyPosition, property.FindPropertyRelative(prop.ToString()));
                        break;
                }
            }
        }
    }
}
