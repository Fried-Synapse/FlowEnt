using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localEulerAngles" /> value.
    /// </summary>
    public class RotateLocalVectorMotion : AbstractVector3Motion<Transform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new RotateLocalVectorMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new RotateLocalVectorMotion(item, from, to);
        }

        public RotateLocalVectorMotion(Transform item, Vector3 value) : base(item, value)
        {
        }

        public RotateLocalVectorMotion(Transform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.localEulerAngles;
        protected override void SetValue(Vector3 value) => item.localEulerAngles = value;
    }
}