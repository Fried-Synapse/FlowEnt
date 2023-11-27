using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IAbstractAnimationBuilder : IListBuilderItem
    {
        public bool IsEnabled { get; }

        public AbstractAnimation Build();
    }

    public abstract class AbstractAnimationBuilder<TAnimation> : AbstractBuilder<TAnimation>, IAbstractAnimationBuilder
        where TAnimation : AbstractAnimation
    {
        [SerializeField]
        private bool isEnabled = true;

        public bool IsEnabled => isEnabled;

        AbstractAnimation IAbstractAnimationBuilder.Build()
            => Build();
    }
}