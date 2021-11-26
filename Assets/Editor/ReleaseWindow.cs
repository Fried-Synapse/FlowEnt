using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityDebug = UnityEngine.Debug;

namespace FriedSynapse.Release
{
    public class ReleaseWindow : EditorWindow
    {
        private ReleaseData ReleaseData { get; set; }
        private Editor ReleaseDataEditor { get; set; }

        [MenuItem("FriedSynapse/Release", false, 1)]
        private static void Init()
        {
            string[] ids = AssetDatabase.FindAssets("t:ReleaseData");
            if (ids == null || ids.Length == 0)
            {
                UnityDebug.LogError("Cannot find Release Data!");
                return;
            }
            if (ids.Length > 1)
            {
                UnityDebug.LogWarning("Multiple release datas found! Using the first one found.");
                return;
            }
            ReleaseWindow window = GetWindow<ReleaseWindow>("Release");
            window.ReleaseData = AssetDatabase.LoadAssetAtPath<ReleaseData>(AssetDatabase.GUIDToAssetPath(ids[0]));
            window.ReleaseDataEditor = Editor.CreateEditor(window.ReleaseData);
            window.Show();
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
            AssetDatabase.ExportPackage($"Assets/{Application.productName}", ReleaseData.GetFilePath(), ExportPackageOptions.Recurse);
            Upload();
        }

        private void Upload()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = Path.Combine(Application.dataPath, "Editor/upload.sh");
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.Arguments = "arg1 arg2 arg3";

            Process p = Process.Start(psi);
            string strOutput = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        }
    }
}