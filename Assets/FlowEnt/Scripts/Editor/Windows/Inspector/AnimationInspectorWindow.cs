using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class AnimationInspectorWindow : FlowEntWindow<AnimationInspectorWindow>
    {
        protected override string Name => Animation.ToString();
        private AbstractAnimation Animation { get; set; }

        internal static void ShowGrouped(AbstractAnimation animation)
        {
            AnimationInspectorWindow window = CreateWindow<AnimationInspectorWindow>(Types);
            window.Animation = animation;
            window.titleContent = new GUIContent(window.Name, window.Logo);
        }

        protected override void CreateGUI()
        {
        }
    }
}