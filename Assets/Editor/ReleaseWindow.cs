using UnityEditor;
using UnityEngine;
using UnityEditorEditor = UnityEditor.Editor;

namespace FriedSynapse.FlowEnt.Builder.Editor
{
    public class ReleaseWindow : EditorWindow
    {
        [SerializeField]
        private ReleaseData releaseData;
        private UnityEditorEditor ReleaseDataEditor { get; set; }

        [MenuItem("FlowEnt/Release", false, 1)]
        private static void Init()
        {
            ReleaseWindow window = GetWindow<ReleaseWindow>("Release");
            window.Show();
            window.ReleaseDataEditor = UnityEditorEditor.CreateEditor(window.releaseData);
        }

        private void OnGUI()
        {
            ReleaseDataEditor.OnInspectorGUI();

            EditorGUILayout.Space(100);

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Release", GUILayout.Width(500), GUILayout.Height(50)))
            {
                Release();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private void Release()
        {
            AssetDatabase.ExportPackage("Assets/FlowEnt", releaseData.GetFilePath(), ExportPackageOptions.Recurse);
        }
    }
}