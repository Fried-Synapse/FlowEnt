using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class ControllableSection : VisualElement
    {
        public ControllableSection(IControllable controllable)
        {
            Controllable = controllable;
        }

        private IControllable Controllable { get; }

    }
}
