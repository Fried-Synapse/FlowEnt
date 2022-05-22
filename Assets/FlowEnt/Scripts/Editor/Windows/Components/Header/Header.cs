using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class Header : VisualElement
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<Header, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private readonly UxmlStringAttributeDescription text = new UxmlStringAttributeDescription { name = "text" };

            public override void Init(VisualElement visualElement, IUxmlAttributes bag, CreationContext context)
            {
                base.Init(visualElement, bag, context);
                Header header = visualElement as Header;
                header.Text = text.GetValueFromBag(bag, context);
            }
        }

        public Header()
        {
            this.LoadUxml();
            TextElement = this.Query<TextElement>("text").First();
        }

        private TextElement TextElement { get; }

        public string Text
        {
            get => TextElement.text;
            set => TextElement.text = value;
        }
    }
}
