using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.rotation.eulerAngles" /> value.
    /// </summary>
    public class RotateVectorMotion : AbstractVector3Motion<Rigidbody>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new RotateVectorMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new RotateVectorMotion(item, from, to);
        }

        public RotateVectorMotion(Rigidbody item, Vector3 value) : base(item, value)
        {
        }

        public RotateVectorMotion(Rigidbody item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.rotation.eulerAngles;
        protected override void SetValue(Vector3 value) => item.rotation = Quaternion.Euler(value);
    }
}
