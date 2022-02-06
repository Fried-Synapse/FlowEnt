using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class FlowEntMenu
    {
        [MenuItem("Tools/FlowEnt/Settings", false, 100)]
        public static void ShowSettings()
        {
            FlowEntSettingsWindow window = EditorWindow.GetWindow<FlowEntSettingsWindow>("FlowEnt Settings");
            window.Show();
        }

        [MenuItem("Tools/FlowEnt/Inspector", false, 101)]
        public static void ShowInspector()
        {
            FlowEntInspectorWindow window = EditorWindow.GetWindow<FlowEntInspectorWindow>("FlowEnt Inspector");
            window.titleContent = new GUIContent("FlowEnt Inspector", Resources.Load<Texture2D>("Logo"));
            window.Show();
        }

        [MenuItem("Tools/FlowEnt/Website", false, 200)]
        public static void GoToWebsite()
        {
            Application.OpenURL("https://flowent.friedsynapse.com/");
        }

        [MenuItem("Tools/FlowEnt/Documentation", false, 201)]
        public static void GoToDocumentation()
        {
            Application.OpenURL("https://docs.flowent.friedsynapse.com/");
        }
    }
}
