using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class SettingsWindow : AbstractThemedWindow<SettingsWindow>
    {
        private const string FlowEntAutoCancel = "FlowEnt_AutoCancel";
        private const string FlowEntDebugEditor = "FlowEnt_Debug_Editor";
        private const string FlowEntDebug = "FlowEnt_Debug";
        protected override string Name => "FlowEnt Settings";

        protected override void CreateGUI()
        {
            LoadHeader();
            LoadContent();
            
            VisualElement runtime = Content.Query<VisualElement>("runtime").First();
            runtime.Add(new FeatureToggle("Auto cancel", FlowEntAutoCancel,
                warning:
                "This will cancel all related animations when an object is destroyed.\nReduces performance by about 5%!"));
            
            VisualElement debug = Content.Query<VisualElement>("debug").First();
            debug.Add(new FeatureToggle("Debugging in Editor", FlowEntDebugEditor));
            debug.Add(new FeatureToggle("Debugging always", FlowEntDebug,
                warning:
                "Debugging mode slows down the animation engine performance. Make sure you do not enable this on your production environment!"));
        }
    }
}