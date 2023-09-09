using UnityEditor;
using UEditor = UnityEditor.Editor;

namespace FriedSynapse.FlowEnt.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FlowEntBuilderContainer))]
    public class FlowEntBuilderContainerEditor : UEditor
    {
        SerializedProperty animations;

        void OnEnable()
        {
            animations = serializedObject.FindProperty("animations");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            serializedObject.ApplyModifiedProperties();
        }
    }
}