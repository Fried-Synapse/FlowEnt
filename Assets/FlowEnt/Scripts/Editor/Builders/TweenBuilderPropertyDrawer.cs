using System.Reflection;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuilder))]
    public class TweenBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Tween, TweenBuilder>
    {
        private const PlayState Started = PlayState.Playing | PlayState.Paused;

        protected override void DrawControls(Rect position, SerializedProperty property)
        {
            float buttonsWidth = DrawControlButtons(position, property);
            Data data = GetData(property);
            float editedPreviewTime = EditorGUI.Slider(FlowEntEditorGUILayout.GetIndentedRect(position, buttonsWidth), data.PreviewTime, 0f, 1f);
            if (editedPreviewTime != data.PreviewTime)
            {
                if (data.PreviewAnimation == null)
                {
                    StartPreview(property);
                    data.PreviewAnimation.Pause();
                }
                else
                {
                    if ((data.PreviewAnimation.PlayState & Started) != 0)
                    {
                        data.PreviewAnimation.Pause();
                    }
                }

                float delta = (editedPreviewTime - data.PreviewTime) * data.PreviewAnimation.Time;
                data.PreviewAnimation.GetType().GetMethod("UpdateInternal", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(data.PreviewAnimation, new object[1] { delta });
                data.PreviewTime = editedPreviewTime;
            }
        }

        protected override Tween Build(SerializedProperty property)
            => property.GetValue<TweenBuilder>().Build();

        protected override void OnAnimationUpdated(Data data, float t)
        {
            data.PreviewTime = data.PreviewAnimation.GetPropertyValue<float>("CurrentLoopRatio");
        }
    }
}
