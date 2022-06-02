using System.Collections;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Reflection;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class InspectorWindow : FlowEntWindow<InspectorWindow>
    {
        protected override string Name => "FlowEnt Inspector";
        private TextField Search { get; set; }
        private VisualElement AnimationsContainer { get; set; }
        internal override void CreateGUI()
        {
            base.CreateGUI();
            Content.Query<VisualElement>("mainControl").First().Add(new ControlSection(FlowEntController.Instance));
            Search = Content.Query<TextField>("search").First();
            AnimationsContainer = Content.Query<VisualElement>("animations").First();
            AnimationsContainer.Add(new AnimationInfoList(FlowEntController.Instance));
            Bind();
        }

        private void Bind()
        {
            Search.RegisterValueChangedCallback(eventData =>
            {
                //TODO
                UnityEngine.Debug.Log($"{eventData.newValue}");
            });
        }
    }
}