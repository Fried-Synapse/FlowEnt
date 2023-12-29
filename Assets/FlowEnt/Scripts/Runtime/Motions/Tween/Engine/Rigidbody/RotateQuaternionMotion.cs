using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Rigidbodies
{
    /// <summary>
    /// Lerps the <see cref="Rigidbody.rotation" /> value.
    /// </summary>
    public class RotateQuaternionMotion : AbstractQuaternionMotion<Rigidbody>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new RotateQuaternionMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new RotateQuaternionMotion(item, From, to);
        }

        public RotateQuaternionMotion(Rigidbody item, Quaternion value) : base(item, value)
        {
        }

        public RotateQuaternionMotion(Rigidbody item, Quaternion? from, Quaternion to) : base(item, from, to)
        {
        }

        protected override Quaternion GetFrom() => item.rotation;
        protected override void SetValue(Quaternion value) => item.rotation = value;
    }
}