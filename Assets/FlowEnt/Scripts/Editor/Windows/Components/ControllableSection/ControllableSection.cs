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

            this.Query<Button>("prevFrame").First().style.backgroundImage = new StyleBackground((Texture2D)Icon.PrevFrame.image);
            this.Query<Button>("play").First().style.backgroundImage = new StyleBackground((Texture2D)Icon.Play.image);
            this.Query<Button>("nextFrame").First().style.backgroundImage = new StyleBackground((Texture2D)Icon.NextFrame.image);
            this.Query<Button>("stop").First().style.backgroundImage = new StyleBackground((Texture2D)Icon.Stop.image);
        }

        private IControllable Controllable { get; }
    }
}