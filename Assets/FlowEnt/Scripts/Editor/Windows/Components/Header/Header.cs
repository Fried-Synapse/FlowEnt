using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    public class Header : AbstractComponent
    {
        protected override bool HasUxml => true;

        public Header(string text)
        {
            this.Query<TextElement>("text").First().text = text;
        }
    }
}
