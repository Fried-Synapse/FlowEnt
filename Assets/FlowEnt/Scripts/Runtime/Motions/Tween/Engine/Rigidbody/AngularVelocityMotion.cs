using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.angularVelocity" /> value.
    /// </summary>
    public class AngularVelocityMotion : AbstractVector3Motion<Rigidbody>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new AngularVelocityMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new AngularVelocityMotion(item, From, to);
        }

        public AngularVelocityMotion(Rigidbody item, Vector3 value) : base(item, value)
        {
        }

        public AngularVelocityMotion(Rigidbody item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.angularVelocity;
        protected override void SetValue(Vector3 value) => item.angularVelocity = value;
    }
}
