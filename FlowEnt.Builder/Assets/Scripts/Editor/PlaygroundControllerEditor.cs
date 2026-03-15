using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder.Editor
{
    [CustomEditor(typeof(PlaygroundController))]
    public class PlaygroundControllerEditor : UnityEditor.Editor
    {
        private PlaygroundController PlaygroundController { get; set; }
        void OnEnable()
        {
            PlaygroundController = (PlaygroundController)target;
        }
        
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Reset"))
            {
                PlaygroundController.ResetMob();
            }
            EditorGUILayout.LabelField("Motion tools");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Tween"))
            {
                PlaygroundController.AddAllMotions<AbstractTweenMotionBuilder>();
            }
            if (GUILayout.Button("Echo"))
            {
                PlaygroundController.AddAllMotions<AbstractEchoMotionBuilder>();
            }
            if (GUILayout.Button("Clear"))
            {
                PlaygroundController.Clear();
            }
            EditorGUILayout.EndHorizontal();

            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
