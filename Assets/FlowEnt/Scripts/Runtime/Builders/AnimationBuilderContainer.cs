using System;
using System.Collections.Generic;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class AnimationBuilderContainer : AbstractListBuilder<IAbstractAnimationBuilder, AbstractAnimation>
    {
        public override List<AbstractAnimation> Build()
            => Items.ConvertAll(m => m.Build());
    }
}