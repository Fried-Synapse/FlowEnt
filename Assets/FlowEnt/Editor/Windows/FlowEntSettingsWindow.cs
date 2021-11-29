using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public class FlowEntSettingsWindow : EditorWindow
    {
        private const string FlowEntDebug = "FlowEnt_Debug";
        private const string FlowEntDebugEditor = "FlowEnt_Debug_Editor";

        private void OnGUI()
        {
            FlowEntEditorGUILayout.Header("FlowEnt Settings");
            EditorGUI.indentLevel++;
            ShowDebug("Debugging in Editor", FlowEntDebugEditor, false);
            ShowDebug("Debugging always", FlowEntDebug, true);
            EditorGUI.indentLevel--;
        }

        private void ShowDebug(string label, string symbol, bool showWarning)
        {
            PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, out string[] defines);
            List<string> definesList = defines.ToList();
            bool wasDebugging = definesList.Contains(symbol);

            bool isDebugging = EditorGUILayout.Toggle(label, wasDebugging);

            if (isDebugging != wasDebugging)
            {
                if (EditorApplication.isPlaying)
                {
                    EditorApplication.isPlaying = false;
                }

                if (isDebugging)
                {
                    definesList.Add(symbol);
                }
                else
                {
                    definesList.Remove(symbol);
                }
            }
            PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, definesList.ToArray());

            if (showWarning && isDebugging)
            {
                EditorGUILayout.HelpBox("Debugging mode slows down the tween performance. Make sure you do not enable this on your producion build!", MessageType.Warning);
            }
        }
    }
}
