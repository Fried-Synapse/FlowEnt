using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="TRigidbody.position" /> value.
    /// </summary>
    public class MoveVectorMotion : AbstractVector3Motion<Rigidbody>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new MoveVectorMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new MoveVectorMotion(item, from, to);
        }

        public MoveVectorMotion(Rigidbody item, Vector3 value) : base(item, value)
        {
        }

        public MoveVectorMotion(Rigidbody item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.position;
        protected override void SetValue(Vector3 value) => item.position = value;
    }
}
