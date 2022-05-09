using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    public static class FlowEntMenu
    {
        private static TEditorWindow ShowWindow<TEditorWindow>(string name)
            where TEditorWindow : EditorWindow
        {
            TEditorWindow window = EditorWindow.GetWindow<TEditorWindow>(name);
            window.titleContent = new GUIContent(name, Resources.Load<Texture2D>("Logo"));
            window.Show();
            return window;
        }

        [MenuItem("Tools/FlowEnt/Settings", false, 100)]
        public static void ShowSettings()
            => ShowWindow<FlowEntSettingsWindow>("FlowEnt Settings");

        [MenuItem("Tools/FlowEnt/Previewer", false, 101)]
        public static void ShowPreviewer()
            => ShowWindow<FlowEntPreviewerWindow>("FlowEnt Previewer");

        [MenuItem("Tools/FlowEnt/Inspector", false, 102)]
        public static void ShowInspector()
            => ShowWindow<FlowEntInspectorWindow>("FlowEnt Inspector");

        [MenuItem("Tools/FlowEnt/Website", false, 200)]
        public static void GoToWebsite()
            => Application.OpenURL("https://flowent.friedsynapse.com/");

        [MenuItem("Tools/FlowEnt/Documentation", false, 201)]
        public static void GoToDocumentation()
            => Application.OpenURL("https://docs.flowent.friedsynapse.com/");
    }
}
