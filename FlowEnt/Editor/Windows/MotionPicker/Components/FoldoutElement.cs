using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace FriedSynapse.FlowEnt.Editor
{
    [UxmlElement]
    public partial class FoldoutScrollable : Foldout
    {
        private ScrollView ScrollView { get; }

        public FoldoutScrollable()
        {
            ScrollView = new ScrollView();
            base.contentContainer.Add(ScrollView);
        }
    }
}