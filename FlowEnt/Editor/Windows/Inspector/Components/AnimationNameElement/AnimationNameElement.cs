using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    internal class AnimationNameElement : VisualElement
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<AnimationNameElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        public AnimationNameElement()
        {
            this.LoadUxml();
            icon = this.Query<VisualElement>("icon").First();
            name = this.Query<Label>("name").First();
        }

        private readonly VisualElement icon;
        private readonly new Label name;
        private AbstractAnimation animation;
        protected AbstractAnimation Animation => animation;
        internal void Init(AbstractAnimation animation)
        {
            if (this.animation != null)
            {
                icon.RemoveFromClassList(this.animation.GetType().Name.ToLower());
            }
            this.animation = animation;
            icon.AddToClassList(this.animation.GetType().Name.ToLower());
            name.text = this.animation.ToString();
        }
    }
}