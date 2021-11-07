using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class FlowEntSettingsWindow : EditorWindow
    {
        private const string FlowEntDebug = "FlowEnt_Debug";

        [MenuItem("FlowEnt/Settings", false, 100)]
        private static void Init()
        {
            FlowEntSettingsWindow window = GetWindow<FlowEntSettingsWindow>("FlowEnt Settings");
            window.Show();
        }

        private void OnGUI()
        {
            FlowEntEditorGUILayout.Header("FlowEnt Settings");
            EditorGUI.indentLevel++;
            ShowDebug();
            EditorGUI.indentLevel--;
        }

        private void ShowDebug()
        {
            PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, out string[] defines);
            List<string> definesList = defines.ToList();
            bool wasDebugging = definesList.Contains(FlowEntDebug);

            bool isDebugging = EditorGUILayout.Toggle("Enable Debugging", wasDebugging);

            if (isDebugging != wasDebugging)
            {
                if (isDebugging)
                {
                    definesList.Add(FlowEntDebug);
                }
                else
                {
                    definesList.Remove(FlowEntDebug);
                }
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, definesList.ToArray());

            if (isDebugging)
            {
                EditorGUILayout.HelpBox("Debugging mode slows down the tween performance. Make sure you do not enable this on your producion build!", MessageType.Warning);
            }
        }
    }
}
