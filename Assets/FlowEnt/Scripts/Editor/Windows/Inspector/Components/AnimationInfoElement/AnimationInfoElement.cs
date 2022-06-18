using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class AnimationInfoElement : VisualElement
    {
        public AnimationInfoElement(AbstractAnimation animation)
        {
            this.LoadUxml();
            this.animation = animation;
            icon = this.Query<VisualElement>("icon").First();
            name = this.Query<Label>("name").First();
            open = this.Query<Button>("open").First();
            if (this.animation is Flow flow)
            {
                List = new AnimationInfoList(flow)
                {
                    name = "sublist"
                };
                showCount = this.Query<Toggle>("showCount").First();
                showCount.SetVisible(true);
                Add(List);
            }
            Init();
            Bind();
        }

        internal readonly AbstractAnimation animation;
        private readonly VisualElement icon;
        private readonly new Label name;
        private readonly Toggle showCount;
        private readonly Button open;
        internal AnimationInfoList List { get; }

        private void Init()
        {
            icon.AddToClassList(animation.GetType().Name.ToLower());
            name.text = animation.ToString();
        }

        private void Bind()
        {
            if (animation is Flow _)
            {
                showCount.RegisterValueChangedCallback(eventData => List.ToggleCount(eventData.newValue));
            }
            open.clicked += () => AnimationInspectorWindow.ShowGrouped(animation);
        }

        internal bool Search(string term)
        {
            bool isMatching = string.IsNullOrEmpty(term)
                || animation.Name?.ToLower().Contains(term) == true
                || animation.GetType().Name.ToLower().Contains(term);
            if (animation is Flow _)
            {
                isMatching = List.Search(term) || isMatching;
            }
            this.SetVisible(isMatching);
            return isMatching;
        }
    }
}
