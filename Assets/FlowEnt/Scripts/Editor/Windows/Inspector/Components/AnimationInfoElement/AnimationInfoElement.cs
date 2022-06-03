using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class AnimationInfoElement : VisualElement
    {
        public AnimationInfoElement(AbstractAnimation animation)
        {
            this.LoadUxml();
            Animation = animation;
            Icon = this.Query<VisualElement>("icon").First();
            Name = this.Query<Label>("name").First();
            Open = this.Query<Button>("open").First();
            if (Animation is Flow flow)
            {
                List = new AnimationInfoList(flow)
                {
                    name = "sublist"
                };
                ShowCount = this.Query<Toggle>("showCount").First();
                ShowCount.SetVisible(true);
                Add(List);
            }
            Init();
            Bind();
        }

        internal AbstractAnimation Animation { get; }
        private VisualElement Icon { get; }
        private Label Name { get; }
        private Toggle ShowCount { get; }
        private Button Open { get; }
        private AnimationInfoList List { get; }

        private void Init()
        {
            Icon.AddToClassList(Animation.GetType().Name.ToLower());
            Name.text = Animation.ToString();
        }

        private void Bind()
        {
            if (Animation is Flow _)
            {
                ShowCount.RegisterValueChangedCallback(eventData => List.ToggleCount(eventData.newValue));
            }
        }

        internal bool Search(string term)
        {
            bool isMatching = string.IsNullOrEmpty(term)
                || Animation.Name?.ToLower().Contains(term) == true
                || Animation.GetType().Name.ToLower().Contains(term);
            if (Animation is Flow _)
            {
                isMatching = List.Search(term) || isMatching;
            }
            this.SetVisible(isMatching);
            return isMatching;
        }
    }
}
