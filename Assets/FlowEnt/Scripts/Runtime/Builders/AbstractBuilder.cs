using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IBuilder<TItem>
    {
        public TItem Build();
    }

    public abstract class AbstractBuilder<TItem> : IBuilder<TItem>
    {
#if UNITY_EDITOR
        //HACK this is because there is a bug in editor lists that doesn't initialise the list item with default values
        //https://issuetracker.unity3d.com/issues/serializefield-list-objects-are-not-initialized-with-class-slash-struct-default-values-when-adding-objects-in-the-inspector-window
        [SerializeField]
        private bool isInit;
#endif
        public abstract TItem Build();
    }

    public interface IBuilderListItem
    {
    }

    public interface IMotionBuilder : IBuilderListItem
    {
        public string DisplayName { get; }
        public bool IsEnabled { get; }
    }
}