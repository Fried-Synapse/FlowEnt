using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoBuilder))]
    public class EchoBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Echo, EchoBuilder>
    {
        protected override void DrawControls(Rect position, SerializedProperty property)
        {
            float buttonsWidth = DrawControlButtons(position, property);
            EditorGUI.LabelField(FlowEntEditorGUILayout.GetIndentedRect(position, buttonsWidth), "Time elapsed", GetData(property).PreviewTime.ToString());
        }

        protected override Echo Build(SerializedProperty property)
            => property.GetValue<EchoBuilder>().Build();

        protected override void OnAnimationUpdated(Data data, float t)
        {
            data.PreviewTime += t;
        }
    }
}
