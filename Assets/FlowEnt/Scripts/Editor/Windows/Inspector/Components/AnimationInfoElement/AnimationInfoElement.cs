using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    public class AnimationInfoElement : VisualElement
    {
        public AnimationInfoElement(AbstractAnimation animation)
        {
            this.LoadUxml();
            Animation = animation;
            Icon = this.Query<VisualElement>("icon").First();
            Name = this.Query<Label>("name").First();
            Open = this.Query<Button>("open").First();
            Init();
            Bind();
        }

        private AbstractAnimation Animation { get; }
        private VisualElement Icon { get; }
        private Label Name { get; }
        private Button Open { get; }

        private void Init()
        {
            Icon.AddToClassList(Animation.GetType().Name.ToLower());
            Name.text = Animation.ToString();
        }

        private void Bind()
        {

        }
    }
}
