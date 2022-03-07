using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(FlowBuilder))]
    public class FlowBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Flow, FlowBuilder>
    {
        private float previewTime;

        protected override List<string> VisibleProperties => new List<string>{
            "options",
            "events",
            "queues",
        };

        protected override void DrawControls(Rect position, SerializedProperty property)
        {
            float buttonsWidth = DrawControlButtons(position, property);
            EditorGUI.LabelField(FlowEntEditorGUILayout.GetIndentedRect(position, buttonsWidth), "Time elapsed", previewTime.ToString());
        }

        protected override Flow Build(SerializedProperty property)
            => property.GetValue<FlowBuilder>().Build(FlowEntEditorController.Instance);

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
