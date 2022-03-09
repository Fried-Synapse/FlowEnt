using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder.Editor
{
    [CustomEditor(typeof(PlaygroundController))]
    public class PlaygroundControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Reset"))
            {
                ((PlaygroundController)target).ResetMob();
            }
            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
