using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoBuilder))]
    public class EchoBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Echo, EchoBuilder>
    {
        private float previewTime;

        protected override void DrawControls(Rect position, SerializedProperty property)
        {
            DrawControlButtons(position, property);

            Rect progressPosition = position;
            float buttonsWidth = EditorGUIUtility.singleLineHeight * 2;
            progressPosition.width -= buttonsWidth;
            progressPosition.x += buttonsWidth;
            EditorGUI.LabelField(progressPosition, "Time elapsed", previewTime.ToString());
        }

        protected override Echo Build(SerializedProperty property)
            => property.GetValue<EchoBuilder>().Build(FlowEntEditorController.Instance);

        protected override void OnAnimationUpdated(float t)
        {
            previewTime += t;
        }

        public override void Reset()
        {
            base.Reset();
            previewTime = 0;
        }
    }
}
