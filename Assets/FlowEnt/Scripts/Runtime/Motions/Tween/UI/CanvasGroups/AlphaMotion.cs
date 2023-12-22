using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.CanvasGroups
{
    /// <summary>
    /// Lerps the <see cref="CanvasGroup.alpha" /> value.
    /// </summary>
    public class AlphaMotion : AbstractAlphaMotion<CanvasGroup>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new AlphaMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new AlphaMotion(item, From, to);
        }
        
        public AlphaMotion(CanvasGroup item, float value) : base(item, value)
        {
        }

        public AlphaMotion(CanvasGroup item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.alpha;
        protected override void SetValue(float value) => item.alpha = value;
    }
}