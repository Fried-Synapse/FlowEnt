using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal abstract class FlowEntWindow<TEditorWindow> : EditorWindow
        where TEditorWindow : FlowEntWindow<TEditorWindow>
    {
        internal abstract string Name { get; }
        internal static TEditorWindow Instance => ShowWindow();
        internal static TEditorWindow ShowWindow()
        {
            TEditorWindow window = GetWindow<TEditorWindow>(false);
            window.titleContent = new GUIContent(window.Name, Resources.Load<Texture2D>("Logo"));
            return window;
        }
    }
}
