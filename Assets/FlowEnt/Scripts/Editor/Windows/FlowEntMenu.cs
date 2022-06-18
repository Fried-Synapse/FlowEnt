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
            return window;
        }

        [MenuItem("Tools/FlowEnt/Settings", false, 100)]
        public static void ShowSettings()
            => SettingsWindow.ShowSingleton();

        [MenuItem("Tools/FlowEnt/Previewer", false, 101)]
        public static void ShowPreviewer()
            => PreviewerWindow.ShowSingleton();

        [MenuItem("Tools/FlowEnt/Inspector", false, 102)]
        public static void ShowInspector()
            => InspectorWindow.ShowSingleton();

        [MenuItem("Tools/FlowEnt/Website", false, 200)]
        public static void GoToWebsite()
            => Application.OpenURL("https://flowent.friedsynapse.com/");

        [MenuItem("Tools/FlowEnt/Documentation", false, 201)]
        public static void GoToDocumentation()
            => Application.OpenURL("https://docs.flowent.friedsynapse.com/");
    }
}
