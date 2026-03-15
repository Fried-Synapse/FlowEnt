using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    [UxmlElement]
    internal partial class Header : VisualElement
    {
        public Header()
        {
            this.LoadUxml();
            TextElement = this.Query<TextElement>("text").First();
        }

        public Header(string text) : this()
        {
            TextElement.text = text;
        }

        private TextElement TextElement { get; }

        [UxmlAttribute("text")]
        public string Text { get => TextElement.text; set => TextElement.text = value; }
    }
}