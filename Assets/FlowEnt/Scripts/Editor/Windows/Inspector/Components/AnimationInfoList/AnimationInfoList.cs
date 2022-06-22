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
            count = this.Query<VisualElement>("count").First();
            tweenCount = this.Query<VisualElement>("tween").First().Query<Label>("value").First();
            echoCount = this.Query<VisualElement>("echo").First().Query<Label>("value").First();
            FlowCount = this.Query<VisualElement>("flow").First().Query<Label>("value").First();
        }

        public AnimationInfoList(FlowEntController flowEntController) : this()
        {
            updateController = flowEntController;
            EditorApplication.update += RenderController;
            ToggleCount(true);
        }

        public AnimationInfoList(Flow flow) : this()
        {
            updateController = flow;
            EditorApplication.update += RenderFlow;
        }

        private readonly SortedList<ulong, AbstractAnimation> animations = new SortedList<ulong, AbstractAnimation>();
        private readonly SortedList<ulong, AnimationInfoListItem> animationElements = new SortedList<ulong, AnimationInfoListItem>();
        private readonly VisualElement count;
        private readonly Label tweenCount;
        private readonly Label echoCount;

        internal void Deinit()
        {
            if (updateController is FlowEntController)
            {
                EditorApplication.update -= RenderController;
            }
            else
            {
                EditorApplication.update -= RenderFlow;
            }
        }

        private readonly Label FlowCount;
        private readonly IUpdateController updateController;

        internal Action OnChanged { get; set; }

        internal void ToggleCount(bool isVisible)
            => count.SetVisible(isVisible);

        internal bool Search(string searchTerm)
        {
            bool isMatching = false;
            foreach (AnimationInfoListItem animationElement in animationElements.Values)
            {
                isMatching = animationElement.Search(searchTerm) || isMatching;
            }
            return isMatching;
        }

        private void ReadAnimations(string field)
        {
            AbstractUpdatable index = updateController.GetUpdatableIndex(field);
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
            UnityEngine.Debug.Log($"xxxx");

            animations.Clear();
            ReadAnimations("updatables");

            Render();
        }

        private void Render()
        {
            List<ulong> oldElementKeys = animationElements.Keys.ToList();
            int tweenCount = 0, echoCount = 0, flowCount = 0;
            bool hasChanged = false;
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

                AnimationInfoListItem element = new AnimationInfoListItem(animation);
                if (element.List != null && OnChanged != null)
                {
                    element.List.OnChanged += OnChanged.Invoke;
                }
                animationElements.Add(animation.Id, element);
                Add(element);
                hasChanged = true;
            }

            foreach (ulong oldElementKey in oldElementKeys)
            {
                animationElements[oldElementKey].List?.Deinit();
                Remove(animationElements[oldElementKey]);
                animationElements.Remove(oldElementKey);
                hasChanged = true;
            }

            this.tweenCount.text = tweenCount.ToString();
            this.echoCount.text = echoCount.ToString();
            FlowCount.text = flowCount.ToString();

            if (hasChanged)
            {
                OnChanged?.Invoke();
            }
        }
    }
}
