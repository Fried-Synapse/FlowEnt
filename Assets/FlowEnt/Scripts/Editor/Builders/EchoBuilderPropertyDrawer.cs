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
    }
}
