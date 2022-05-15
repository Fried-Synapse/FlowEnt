using UnityEngine;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    public class ControllableSection : AbstractComponent
    {
        protected override bool HasUxml => true;

        public ControllableSection(IControllable controllable)
        {
            Controllable = controllable;

            SetIcon("prevFrame", Icon.PrevFrame);
            SetIcon("play", Icon.Play);
            SetIcon("nextFrame", Icon.NextFrame);
            SetIcon("stop", Icon.Stop);

        }

        private void SetIcon(string selector, GUIContent icon)
            => this
                .Query<Button>(selector).First()
                .Query<VisualElement>("icon").First()
                .style.backgroundImage = new StyleBackground((Texture2D)icon.image);

        private IControllable Controllable { get; }
    }
}