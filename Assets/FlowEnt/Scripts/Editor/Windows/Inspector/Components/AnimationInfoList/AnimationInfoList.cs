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

        public AnimationInfoList(FlowEntController controller) : this()
        {
            EditorApplication.update += Render;
        }

        public AnimationInfoList(Flow flow) : this()
        {
        }

        private readonly SortedList<ulong, AbstractAnimation> animations = new SortedList<ulong, AbstractAnimation>();
        private readonly SortedList<ulong, AnimationInfoElement> animationElements = new SortedList<ulong, AnimationInfoElement>();
        private Label TweenCount { get; }
        private Label EchoCount { get; }
        private Label FlowCount { get; }

        private void Render()
        {
            void readAnimations(string group) => readAnimations1(FlowEntController.Instance.GetUpdatableIndex(group));
            void readAnimations1(AbstractUpdatable index)
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

            animations.Clear();
            readAnimations("updatables");
            readAnimations("smoothUpdatables");
            readAnimations("lateUpdatables");
            readAnimations("smoothLateUpdatables");
            readAnimations("fixedUpdatables");
            readAnimations("customUpdatables");

            Clear();
            foreach (AbstractAnimation animation in animations.Values)
            {
                Add(new AnimationInfoElement(animation));
            }
        }
    }
}
