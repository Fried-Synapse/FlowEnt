using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.rotation" /> value.
    /// </summary>
    public class RotateQuaternionMotion : AbstractQuaternionMotion<Transform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new RotateQuaternionMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new RotateQuaternionMotion(item, from, to);
        }

        public RotateQuaternionMotion(Transform item, Quaternion value) : base(item, value)
        {
        }

        public RotateQuaternionMotion(Transform item, Quaternion? from, Quaternion to) : base(item, from, to)
        {
        }

        protected override Quaternion GetFrom() => item.rotation;
        protected override void SetValue(Quaternion value) => item.rotation = value;
    }
}