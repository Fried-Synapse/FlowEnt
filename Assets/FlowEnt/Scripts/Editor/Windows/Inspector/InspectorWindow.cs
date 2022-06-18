using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class InspectorWindow : FlowEntWindow<InspectorWindow>
    {
        protected override string Name => "FlowEnt Inspector";
        private TextField search;
        private ScrollView animationsScroll;
        private AnimationInfoList animationList;
        private string searchTerm;

        internal string SearchTerm
        {
            get => searchTerm;
            private set
            {
                searchTerm = value;
                TriggerSearch();
            }
        }
        protected override void CreateGUI()
        {
            LoadHeader();
            LoadContent();
            Content.Query<VisualElement>("mainControl").First().Add(new ControlSection(FlowEntController.Instance));
            search = Content.Query<TextField>("search").First();
            animationsScroll = Content.Query<ScrollView>("animations").First();
            animationList = new AnimationInfoList(FlowEntController.Instance);
            animationsScroll.contentContainer.Add(animationList);
            Bind();
        }

        private void Bind()
        {
            //TODO remove all animations on ExitPlayMode
            animationList.OnChanged += TriggerSearch;
            search.RegisterValueChangedCallback(eventData => SearchTerm = eventData.newValue.ToLower());
        }

        private void TriggerSearch()
        {
            animationList.Search(searchTerm);
        }
    }
}