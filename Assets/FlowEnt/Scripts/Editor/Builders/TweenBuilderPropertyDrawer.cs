using System;
using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuilder))]
    public class TweenBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Tween>
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
                if (previewAnimation?.PlayState.HasFlag(Started) == true)
                {
                    base.Reset();
                }

                if (previewAnimation == null)
                {
                    previewAnimation = property.GetValue<TweenBuilder>().Build(FlowEntEditorController.Instance);
                }

                previewTime = editedPreviewTime;
                previewAnimation.SetT(previewTime);
            }
        }

        protected override Tween Build(SerializedProperty property)
            => property.GetValue<TweenBuilder>().Build(FlowEntEditorController.Instance);

        protected override UnityEngine.Object[] GetObjects(Tween animation)
        {
            ITweenMotion[] motions = animation.GetFieldValue<ITweenMotion[]>("motions");
            List<UnityEngine.Object> result = new List<UnityEngine.Object>();
            foreach (ITweenMotion motion in motions)
            {

            }
            return result.ToArray();
        }

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
