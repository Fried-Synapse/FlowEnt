using UnityEditor;
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
            Content.Query<ControlSection>("control").First().Init(FlowEntController.Instance);
            search = Content.Query<TextField>("search").First();
            animationsScroll = Content.Query<ScrollView>("animations").First();
            animationList = new AnimationInfoList(FlowEntController.Instance);
            animationsScroll.contentContainer.Add(animationList);
            Bind();
        }

        private void Bind()
        {
            EditorApplication.playModeStateChanged += _ =>
            {
                animationList?.Deinit();
                animationList.Clear();
            };
            animationList.OnChanged += TriggerSearch;
            search.RegisterValueChangedCallback(eventData => SearchTerm = eventData.newValue.ToLower());
        }

        private void TriggerSearch()
        {
            animationList.Search(searchTerm);
        }

#pragma warning disable IDE0051, RCS1213
        private void OnDestroy()
        {
            animationList?.Deinit();
        }
#pragma warning restore IDE0051, RCS1213
    }
}