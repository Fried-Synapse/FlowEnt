using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class TweenInspectorWindow : AbstractAnimationInspectorWindow<TweenInspectorWindow, Tween>
    {
        protected override void OnGuiInternal()
        {
            GUILayout.Space(10);

            FlowEntEditorGUILayout.LabelFieldBold("Motions:");
            EditorGUI.indentLevel++;
            foreach (IMotion motion in Animation.GetFieldValue<IMotion[]>("motions"))
            {
                FlowEntEditorGUILayout.LabelField(motion);
            }
            EditorGUI.indentLevel--;
        }
    }
}
