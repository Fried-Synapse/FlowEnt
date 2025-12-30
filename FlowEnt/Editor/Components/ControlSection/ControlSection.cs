using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class ControlSection : AbstractControlSection
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<ControlSection, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }
    }
}