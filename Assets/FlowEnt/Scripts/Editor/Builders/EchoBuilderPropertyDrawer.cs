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
            float buttonsWidth = DrawControlButtons(position, property);
            EditorGUI.LabelField(FlowEntEditorGUILayout.GetIndentedRect(position, buttonsWidth), "Time elapsed", previewTime.ToString());
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
