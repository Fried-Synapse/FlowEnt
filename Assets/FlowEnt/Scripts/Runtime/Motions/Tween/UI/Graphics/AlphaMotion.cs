using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Motions.Tween.UI.Graphics
{
    /// <summary>
    /// Lerps the alpha for <see cref="Graphic.color" /> value.
    /// </summary>
    public class AlphaMotion : AbstractAlphaMotion<Graphic>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new AlphaMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new AlphaMotion(item, From, to);
        }

        public AlphaMotion(Graphic item, float value) : base(item, value)
        {
        }

        public AlphaMotion(Graphic item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.color.a;
        protected override void SetValue(float value) => item.color = SetAlpha(item.color, value);
    }
}