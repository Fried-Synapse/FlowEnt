using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class SettingsWindow : AbstractThemedWindow<SettingsWindow>
    {
        private const string FlowEntDebugEditor = "FlowEnt_Debug_Editor";
        private const string FlowEntDebug = "FlowEnt_Debug";
        private const string FlowEntNullables = "FlowEnt_3_Nullables";
        protected override string Name => "FlowEnt Settings";

        protected override void CreateGUI()
        {
            LoadHeader();
            LoadContent();

            VisualElement debug = Content.Query<VisualElement>("debug").First();
            debug.Add(new FeatureToggle("Debugging in Editor", FlowEntDebugEditor));
            debug.Add(new FeatureToggle("Debugging always", FlowEntDebug,
                warning:
                "Debugging mode slows down the animation engine performance. Make sure you do not enable this on your production environment!"));

            VisualElement features = Content.Query<VisualElement>("features").First();
            features.Add(new HelpBox("Changing any of these might cause loss of data", HelpBoxMessageType.Warning));
            features.Add(new FeatureToggle("Nullables", FlowEntNullables,
                "Enable this to make nullable option for From values available."));
        }
    }
}