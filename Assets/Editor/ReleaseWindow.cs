using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityDebug = UnityEngine.Debug;

namespace FriedSynapse.Release
{
    public class ReleaseWindow : EditorWindow
    {
        private ReleaseData ReleaseData { get; set; }
        private Editor ReleaseDataEditor { get; set; }

        private static string[] GetAssetsIds()
            => AssetDatabase.FindAssets("t:ReleaseData");

        [MenuItem("FriedSynapse/Release", false, 1)]
        private static void Open()
        {
            string[] ids = GetAssetsIds();
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
            window.Show();
        }

        private void OnGUI()
        {
            Init();

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

        private void Init()
        {
            if (ReleaseData != null)
            {
                return;
            }
            string[] ids = GetAssetsIds();
            ReleaseData = AssetDatabase.LoadAssetAtPath<ReleaseData>(AssetDatabase.GUIDToAssetPath(ids[0]));
            ReleaseDataEditor = Editor.CreateEditor(ReleaseData);
        }

        private void Release()
        {
            AssetDatabase.ExportPackage($"Assets/{Application.productName}", ReleaseData.GetFilePath(), ExportPackageOptions.Recurse);
            Upload();
        }

        private void Upload()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "/bin/sh";
            psi.WorkingDirectory = Path.Combine(Application.dataPath, "Editor");
            psi.Arguments = "upload.sh " +
                $"\'{Application.productName}\' " +
                $"\'{ReleaseData.ReleaseVersion}\' " +
                $"\'{ReleaseData.Destination}\' " +
                $"\'{ReleaseData.GetFileName()}\' ";
            psi.WindowStyle = ProcessWindowStyle.Minimized;
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            Process p = Process.Start(psi);
            p.WaitForExit();
            string strOutput = p.StandardOutput.ReadToEnd();
            UnityDebug.Log($"{strOutput}");
        }
    }
}