using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal static class ControlTypeExtensions
    {
        public static string ToClassName(this ControlButton.ControlType type)
        {
            string typeName = type.ToString();
            return char.ToLower(typeName[0]) + typeName.Substring(1);
        }
    }

    internal class ControlButton : Button
    {
        public enum ControlType
        {
            None,
            Play,
            Replay,
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
            this.LoadUss();
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
