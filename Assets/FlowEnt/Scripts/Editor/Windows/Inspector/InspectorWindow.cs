using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class InspectorWindow : FlowEntWindow<InspectorWindow>
    {
        protected override string Name => "FlowEnt Inspector";
        private TextField Search { get; set; }
        private ScrollView AnimationsScroll { get; set; }
        private AnimationInfoList AnimationList { get; set; }
        protected override void CreateGUI()
        {
            LoadHeader();
            LoadContent();
            Content.Query<VisualElement>("mainControl").First().Add(new ControlSection(FlowEntController.Instance));
            Search = Content.Query<TextField>("search").First();
            AnimationsScroll = Content.Query<ScrollView>("animations").First();
            AnimationList = new AnimationInfoList(FlowEntController.Instance);
            AnimationsScroll.contentContainer.Add(AnimationList);
            Bind();
        }

        private void Bind()
        {
            //TODO apply search term on newly created games
            //TODO remove all animations on ExitPlayMode
            Search.RegisterValueChangedCallback(eventData => AnimationList.Search(eventData.newValue.ToLower()));
        }
    }
}