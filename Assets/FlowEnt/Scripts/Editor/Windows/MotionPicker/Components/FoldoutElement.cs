using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    public class FoldoutScrollable : Foldout
    {
        [Preserve]
        public new class UxmlFactory : UxmlFactory<FoldoutScrollable, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : Foldout.UxmlTraits
        {
        }

        private ScrollView ScrollView { get; }

        public override VisualElement contentContainer => ScrollView.contentContainer;

        public FoldoutScrollable()
        {
            ScrollView = new ScrollView();
            base.contentContainer.Add(ScrollView);
        }
    }
}