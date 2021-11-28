using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityDebug = UnityEngine.Debug;

namespace FriedSynapse.Release
{
    public class ReleaseWindow : EditorWindow
    {
        private const string VersionRegex = "version=\"([^\"]*)\"";
        private static string ConfigPath => Path.GetFullPath(Path.Combine(Application.dataPath, "../../config.sh"));
        private string Version { get; set; }
        private string Destination => $"/Users/Shared/Work/FriedSynapse/Builds/{Application.productName}/Packages";
        private string FileName => $"{Application.productName}.{Version}.unitypackage";
        private string FilePath => Path.Combine(Destination, FileName);
        private const int Margin = 50;
        private GUIStyle ContentStyle => new GUIStyle() { margin = new RectOffset(Margin, Margin, 0, 0) };
        private float LabelWidth => 60;
        private float LabelContentWidth => position.width - (2 * Margin) - LabelWidth;

        [MenuItem("FriedSynapse/Release", true, 1)]
        private static bool ValidateOpen()
            => File.Exists(ConfigPath);

        [MenuItem("FriedSynapse/Release", false, 1)]
        private static void Open()
        {
            ReleaseWindow window = GetWindow<ReleaseWindow>("Release");
            window.Show();
        }

        #region GUI

        private void OnGUI()
        {
            Init();

            EditorGUILayout.Space(40);

            ReleaseVersionField();

            EditorGUILayout.Space(10);

            ShowDestination();

            EditorGUILayout.Space(40);

            ShowReleaseButton();
        }

        private void ReleaseVersionField()
        {
            EditorGUILayout.BeginHorizontal(ContentStyle);
            GUILayout.FlexibleSpace();

            EditorGUILayout.LabelField("Version:", GUILayout.Width(LabelWidth));
            EditorGUILayout.LabelField(Version, GUILayout.Width(LabelContentWidth - 100));
            string[] versionParts = Version.Substring(1).Split('.');
            int major = int.Parse(versionParts[0]);
            int minor = int.Parse(versionParts[1]);
            int patch = int.Parse(versionParts[2]);

            GUILayout.BeginVertical(GUILayout.ExpandWidth(false));
            {
                static int getNumber(string name, int value)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(name, GUILayout.Width(35));
                    if (GUILayout.Button("^", GUILayout.MaxWidth(20)))
                    {
                        value++;
                    }

                    if (GUILayout.Button("˅", GUILayout.MaxWidth(20)))
                    {
                        value--;
                    }
                    if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
                    {
                        value = 0;
                    }

                    GUILayout.EndVertical();

                    return value;
                }

                major = getNumber("major", major);
                minor = getNumber("minor", minor);
                patch = getNumber("patch", patch);
            }

            GUILayout.EndVertical();

            string version = $"v{major}.{minor}.{patch}";
            if (Version != version)
            {
                Version = version;
                SaveVersion();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void ShowDestination()
        {
            EditorGUILayout.BeginHorizontal(ContentStyle);
            GUILayout.FlexibleSpace();
            {
                EditorGUILayout.LabelField("File Path:", GUILayout.Width(LabelWidth));
                EditorGUILayout.LabelField(FilePath, GUILayout.Width(LabelContentWidth));
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void ShowReleaseButton()
        {
            GUILayout.BeginHorizontal(ContentStyle);
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Release", GUILayout.Width(position.width - (2 * Margin) - 100), GUILayout.Height(50)))
            {
                Release();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        #endregion

        #region Utils

        private void Init()
        {
            if (Version != null)
            {
                return;
            }
            string config = File.ReadAllText(ConfigPath);
            Match result = new Regex(VersionRegex).Match(config);
            Version = result.Groups[1].Value;
        }

        private void SaveVersion()
        {
            string config = File.ReadAllText(ConfigPath);
            config = new Regex(VersionRegex).Replace(config, $"version=\"{Version}\"");
            File.WriteAllText(ConfigPath, config);
        }

        private void Release()
        {
            AssetDatabase.ExportPackage($"Assets/{Application.productName}", FilePath, ExportPackageOptions.Recurse);
            Upload();
        }

        private void Upload()
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "/bin/sh";
            psi.WorkingDirectory = Path.Combine(Application.dataPath, "Editor");
            psi.Arguments = "upload.sh " +
                $"\'{Application.productName}\' " +
                $"\'{Destination}\' " +
                $"\'{FileName}\' ";
            psi.WindowStyle = ProcessWindowStyle.Minimized;
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            Process p = Process.Start(psi);
            p.WaitForExit();
            string strOutput = p.StandardOutput.ReadToEnd();
            UnityDebug.Log($"{strOutput}");
        }

        #endregion
    }
}