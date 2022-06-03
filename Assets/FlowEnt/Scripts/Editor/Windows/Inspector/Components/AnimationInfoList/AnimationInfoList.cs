using System.Collections.Generic;
using System.Linq;
using FriedSynapse.FlowEnt.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    public class AnimationInfoList : VisualElement
    {
        private AnimationInfoList()
        {
            TweenCount = this.Query<Label>("tween").First();
            EchoCount = this.Query<Label>("echo").First();
            FlowCount = this.Query<Label>("flow").First();
        }

        public AnimationInfoList(FlowEntController _) : this()
        {
            EditorApplication.update += RenderController;
        }

        public AnimationInfoList(Flow flow) : this()
        {
            Flow = flow;
            EditorApplication.update += RenderFlow;
        }

        private readonly SortedList<ulong, AbstractAnimation> animations = new SortedList<ulong, AbstractAnimation>();
        private readonly SortedList<ulong, AnimationInfoElement> animationElements = new SortedList<ulong, AnimationInfoElement>();
        private Label TweenCount { get; }
        private Label EchoCount { get; }
        private Label FlowCount { get; }
        private Flow Flow { get; }

        private void ReadAnimations(AbstractUpdatable index)
        {
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
            void readAnimations(string group) => ReadAnimations(FlowEntController.Instance.GetUpdatableIndex(group));

            animations.Clear();
            readAnimations("updatables");
            readAnimations("smoothUpdatables");
            readAnimations("lateUpdatables");
            readAnimations("smoothLateUpdatables");
            readAnimations("fixedUpdatables");
            readAnimations("customUpdatables");

            Render();
        }

        private void RenderFlow()
        {
            animations.Clear();
            ReadAnimations(Flow);
            Render();
        }

        private void Render()
        {
            List<ulong> oldElementKeys = animationElements.Keys.ToList();
            foreach (AbstractAnimation animation in animations.Values)
            {
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
        }
    }
}
