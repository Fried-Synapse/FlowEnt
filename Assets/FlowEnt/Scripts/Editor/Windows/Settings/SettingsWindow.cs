namespace FriedSynapse.FlowEnt.Editor
{
    internal class SettingsWindow : FlowEntWindow<SettingsWindow>
    {
        private const string FlowEntDebugEditor = "FlowEnt_Debug_Editor";
        private const string FlowEntDebug = "FlowEnt_Debug";
        protected override string Name => "FlowEnt Settings";
        protected override void CreateGUI()
        {
            LoadHeader();
            LoadContent();
            Content.Add(new DebugToggle("Debugging in Editor", FlowEntDebugEditor));
            Content.Add(new DebugToggle("Debugging always", FlowEntDebug, "Debugging mode slows down the animation engine performance. Make sure you do not enable this on your producion environment!"));
        }
    }
}