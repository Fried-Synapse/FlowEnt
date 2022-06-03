using System;
using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class AnimationInfoList : VisualElement
    {
        private AnimationInfoList()
        {
            this.LoadUxml();
            Count = this.Query<VisualElement>("count").First();
            TweenCount = this.Query<VisualElement>("tween").First().Query<Label>("value").First();
            EchoCount = this.Query<VisualElement>("echo").First().Query<Label>("value").First();
            FlowCount = this.Query<VisualElement>("flow").First().Query<Label>("value").First();
        }

        public AnimationInfoList(FlowEntController flowEntController) : this()
        {
            UpdateController = flowEntController;
            EditorApplication.update += RenderController;
            ToggleCount(true);
        }

        public AnimationInfoList(Flow flow) : this()
        {
            UpdateController = flow;
            EditorApplication.update += RenderFlow;
        }

        private readonly SortedList<ulong, AbstractAnimation> animations = new SortedList<ulong, AbstractAnimation>();
        private readonly SortedList<ulong, AnimationInfoElement> animationElements = new SortedList<ulong, AnimationInfoElement>();
        private VisualElement Count { get; }
        private Label TweenCount { get; }
        private Label EchoCount { get; }
        private Label FlowCount { get; }
        private IUpdateController UpdateController { get; }

        internal void ToggleCount(bool isVisible)
            => Count.SetVisible(isVisible);

        internal bool Search(string term)
        {
            bool isMatching = false;
            foreach (AnimationInfoElement animationElement in animationElements.Values)
            {
                isMatching = animationElement.Search(term) || isMatching;
            }
            return isMatching;
        }

        private void ReadAnimations(string field)
        {
            var index = UpdateController.GetUpdatableIndex(field);
            while (index != null)
            {
                if (index is AbstractAnimation animation && !animations.ContainsKey(animation.Id))
                {
                    animations.Add(animation.Id, animation);
                }
                index = index.GetFieldValue<AbstractUpdatable>("next");
            }
        }

        private void RenderController()
        {
            animations.Clear();
            ReadAnimations("updatables");
            ReadAnimations("smoothUpdatables");
            ReadAnimations("lateUpdatables");
            ReadAnimations("smoothLateUpdatables");
            ReadAnimations("fixedUpdatables");
            ReadAnimations("customUpdatables");

            Render();
        }

        private void RenderFlow()
        {
            animations.Clear();
            ReadAnimations("updatables");

            Render();
        }

        private void Render()
        {
            List<ulong> oldElementKeys = animationElements.Keys.ToList();
            int tweenCount = 0, echoCount = 0, flowCount = 0;
            foreach (AbstractAnimation animation in animations.Values)
            {
                switch (animation)
                {
                    case Tween _:
                        ++tweenCount;
                        break;
                    case Echo _:
                        ++echoCount;
                        break;
                    case Flow _:
                        ++flowCount;
                        break;
                }

                if (animationElements.ContainsKey(animation.Id))
                {
                    oldElementKeys.Remove(animation.Id);
                    continue;
                }

                AnimationInfoElement element = new AnimationInfoElement(animation);
                animationElements.Add(animation.Id, element);
                Add(element);
            }

            foreach (ulong oldElementKey in oldElementKeys)
            {
                Remove(animationElements[oldElementKey]);
                animationElements.Remove(oldElementKey);
            }

            TweenCount.text = tweenCount.ToString();
            EchoCount.text = echoCount.ToString();
            FlowCount.text = flowCount.ToString();
        }
    }
}
