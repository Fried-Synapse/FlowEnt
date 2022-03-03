using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.velocity" /> value.
    /// </summary>
    public class VelocityMotion : AbstractVector3Motion<Rigidbody>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new VelocityMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new VelocityMotion(item, from, to);
        }

        public VelocityMotion(Rigidbody item, Vector3 value) : base(item, value)
        {
        }

        public VelocityMotion(Rigidbody item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.velocity;
        protected override void SetValue(Vector3 value) => item.velocity = value;
    }
}
