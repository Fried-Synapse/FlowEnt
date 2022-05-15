using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    public class Header : VisualElement
    {
        public Header(string text)
        {
            this.LoadUss();

            VisualElement wrapper = new VisualElement();
            wrapper.name = nameof(wrapper);
            Add(wrapper);

            Image logo = new Image();
            logo.name = nameof(logo);
            wrapper.Add(logo);

            TextElement textElement = new TextElement();
            textElement.name = "text";
            textElement.text = text;
            wrapper.Add(textElement);
        }

    }
}
