using System;
using System.Collections.Generic;
using System.Linq;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class AnimationListBuilder : AbstractListBuilder<IAbstractAnimationBuilder, AbstractAnimation>
    {
        public override List<AbstractAnimation> Build()
            => Items.Where(m => m.IsEnabled).Select(m => m.Build()).ToList();
    }
}