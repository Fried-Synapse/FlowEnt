using FriedSynapse.FlowEnt.Reflection;
using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class EchoInspectorWindow : AbstractAnimationInspectorWindow<EchoInspectorWindow, Echo>
    {
        protected override void OnGuiInternal()
        {
            GUILayout.Space(10);

            FlowEntEditorGUILayout.LabelFieldBold("Motions:");
            EditorGUI.indentLevel++;
            foreach (IEchoMotion motion in Animation.GetFieldValue<IEchoMotion[]>("motions"))
            {
                FlowEntEditorGUILayout.LabelField(motion);
            }
            EditorGUI.indentLevel--;
        }
    }
}
