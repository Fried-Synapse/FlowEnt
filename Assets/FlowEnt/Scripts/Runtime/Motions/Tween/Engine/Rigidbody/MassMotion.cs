using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.mass" /> value.
    /// </summary>
    public class MassMotion : AbstractFloatMotion<Rigidbody>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new MassMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new MassMotion(item, From, to);
        }

        public MassMotion(Rigidbody item, float value) : base(item, value)
        {
        }

        public MassMotion(Rigidbody item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.mass;
        protected override void SetValue(float value) => item.mass = value;
    }
}