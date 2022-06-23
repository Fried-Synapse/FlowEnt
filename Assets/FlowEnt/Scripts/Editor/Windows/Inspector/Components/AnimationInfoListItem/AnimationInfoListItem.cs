using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class AnimationInfoListItem : AnimationNameElement
    {
        public AnimationInfoListItem(AbstractAnimation animation)
        {
            open = this.Query<Button>("open").First();
            Init(animation);
            if (Animation is Flow flow)
            {
                List = new AnimationInfoList(flow)
                {
                    name = "sublist"
                };
                showCount = this.Query<Toggle>("showCount").First();
                showCount.SetVisible(true);
                Add(List);
            }
            Bind();
        }

        private readonly Toggle showCount;
        private readonly Button open;
        internal AnimationInfoList List { get; }

        private void Bind()
        {
            if (Animation is Flow _)
            {
                showCount.RegisterValueChangedCallback(eventData => List.ToggleCount(eventData.newValue));
            }
            open.clicked += () => AnimationInspectorWindow.ShowGrouped(Animation);
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
