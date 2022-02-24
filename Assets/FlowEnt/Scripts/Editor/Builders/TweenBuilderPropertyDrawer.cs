using System.Reflection;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuilder))]
    public class TweenBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Tween, ITweenMotion>
    {
        private const PlayState Started = PlayState.Playing | PlayState.Paused;
        private float previewTime;

        protected override void DrawControls(Rect position, SerializedProperty property)
        {
            DrawButtons(position, property);

            Rect progressPosition = position;
            float buttonsWidth = EditorGUIUtility.singleLineHeight * 2;
            progressPosition.width -= buttonsWidth;
            progressPosition.x += buttonsWidth;

            float editedPreviewTime = EditorGUI.Slider(progressPosition, previewTime, 0f, 1f);
            if (editedPreviewTime != previewTime)
            {
                if (previewAnimation == null)
                {
                    previewAnimation = property.GetValue<TweenBuilder>().Build(FlowEntEditorController.Instance);
                    RecordUndo();
                    previewAnimation.OnCompleting(() => Debug.Log($"fuck"));
                    previewAnimation.Start();
                    previewAnimation.Pause();
                }
                else
                {
                    if ((previewAnimation.PlayState & Started) != 0)
                    {
                        previewAnimation.Pause();
                    }
                }

                float delta = (editedPreviewTime - previewTime) * previewAnimation.Time;
                previewAnimation.GetType().GetMethod("UpdateInternal", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(previewAnimation, new object[1] { delta });
                previewTime = editedPreviewTime;
            }
        }

        protected override Tween Build(SerializedProperty property)
            => property.GetValue<TweenBuilder>().Build(FlowEntEditorController.Instance);

        protected override void OnAnimationUpdated(float t)
        {
            previewTime = t;
        }

        protected override void Reset()
        {
            base.Reset();
            previewTime = 0;
        }
    }
}
