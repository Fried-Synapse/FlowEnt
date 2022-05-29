using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal abstract class FlowEntWindow<TEditorWindow> : EditorWindow
        where TEditorWindow : FlowEntWindow<TEditorWindow>
    {
#pragma warning disable IDE0044, RCS1169
        [SerializeField]
        private Texture2D logo;
        [SerializeField]
        private VisualTreeAsset content;
#pragma warning restore IDE0044, RCS1169
        protected abstract string Name { get; }
        internal static TEditorWindow Instance { get; private set; }

#pragma warning disable RCS1158
        internal static void ShowWindow()
        {
            if (Instance == null)
            {
                Instance = GetWindow<TEditorWindow>();
                Instance.titleContent = new GUIContent(Instance.Name, Instance.logo);
            }
            else
            {
                Instance.Focus();
            }
        }
#pragma warning restore RCS1158

#pragma warning disable IDE0051, RCS1213
        private void OnEnable()
        {
            Instance ??= (TEditorWindow)this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
#pragma warning restore IDE0051, RCS1213

        internal virtual void CreateGUI()
        {
            rootVisualElement.LoadUxml(content);
        }
    }
}
