using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class ControlButton : Button
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

        [Preserve]
        public new class UxmlFactory : UxmlFactory<ControlButton, UxmlTraits> { }
        [Preserve]
        public new class UxmlTraits : Button.UxmlTraits
        {
            private readonly UxmlEnumAttributeDescription<ControlType> type = new UxmlEnumAttributeDescription<ControlType> { name = "type" };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                ControlButton button = visualElement as ControlButton;
                button.Type = type.GetValueFromBag(bag, context);
            }
        }

        public ControlButton()
        {
            this.LoadUxml();
            IconElement = this.Query<VisualElement>("icon").First();
        }

        private VisualElement IconElement { get; }
        private ControlType type;
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
