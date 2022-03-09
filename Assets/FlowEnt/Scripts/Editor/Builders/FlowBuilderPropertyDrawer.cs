using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(FlowBuilder))]
    public class FlowBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Flow, FlowBuilder>
    {
        protected override List<string> VisibleProperties => new List<string>{
            "options",
            "events",
            "queues",
        };

        protected override void DrawControls(Rect position, SerializedProperty property)
        {
            float buttonsWidth = DrawControlButtons(position, property);
            EditorGUI.LabelField(FlowEntEditorGUILayout.GetIndentedRect(position, buttonsWidth), "Time elapsed", GetData(property).PreviewTime.ToString());
        }

        protected override Flow Build(SerializedProperty property)
            => property.GetValue<FlowBuilder>().Build();

        protected override void OnAnimationUpdated(Data data, float t)
        {
            data.PreviewTime += t;
        }
    }
}
