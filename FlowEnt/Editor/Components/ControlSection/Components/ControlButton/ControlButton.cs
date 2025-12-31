using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    [UxmlElement]
    internal partial class ControlButton : Button
    {
        public enum ControlType
        {
            None,
            Play,
            Pause,
            PrevFrame,
            NextFrame,
            Stop
        }

        public ControlButton()
        {
            this.LoadUxml();
            IconElement = this.Query<VisualElement>("icon").First();
        }

        private VisualElement IconElement { get; }
        private ControlType type;
        [UxmlAttribute("type")]
        public ControlType Type
        {
            get => type;
            set
            {
                IconElement.RemoveFromClassList(type.ToClassName());
                type = value;
                IconElement.AddToClassList(type.ToClassName());
            }
        }
    }
}
