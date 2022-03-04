using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localRotation" /> value.
    /// </summary>
    public class RotateLocalQuaternionMotion : AbstractQuaternionMotion<Transform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new RotateLocalQuaternionMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new RotateLocalQuaternionMotion(item, from, to);
        }

        public RotateLocalQuaternionMotion(Transform item, Quaternion value) : base(item, value)
        {
        }

        public RotateLocalQuaternionMotion(Transform item, Quaternion? from, Quaternion to) : base(item, from, to)
        {
        }

        protected override Quaternion GetFrom() => item.localRotation;
        protected override void SetValue(Quaternion value) => item.localRotation = value;
    }
}