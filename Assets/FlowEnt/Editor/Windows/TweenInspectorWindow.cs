using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
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
            foreach (ITweenMotion motion in Animation.GetFieldValue<ITweenMotion[]>("motions"))
            {
                FlowEntEditorGUILayout.LabelField(motion);
            }
            EditorGUI.indentLevel--;
        }
    }
}
