using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(TweenBuilder))]
    public class TweenBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer
    {
        private float previewTime;
        private Tween previewTween;
        protected override void DrawControls(Rect position, SerializedProperty property)
        {
            float playButtonWidth = EditorGUIUtility.singleLineHeight;
            Rect playButtonPosition = position;
            playButtonPosition.width = playButtonWidth;
            if (GUI.Button(playButtonPosition, Icon.Play, Icon.Style))
            {
                previewTween = property.GetValue<TweenBuilder>().Build(FlowEntEditorController.Instance);
                previewTween.OnUpdating(t =>
                {
                    previewTime = t;
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                });
                previewTween.SetFieldValue("updateController", FlowEntEditorController.Instance);
                previewTween.Start();
            }

            Rect progressPosition = position;
            progressPosition.width -= playButtonWidth;
            progressPosition.x += playButtonWidth;

            previewTime = EditorGUI.Slider(progressPosition, previewTime, 0f, 1f);
        }

        protected override void OnScopeChanged()
        {
            previewTween = null;
            previewTime = 0;
        }
    }
}
