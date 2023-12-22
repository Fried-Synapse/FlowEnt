using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.eulerAngles" /> value.
    /// </summary>
    public class RotateVectorMotion : AbstractVector3Motion<Transform>
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
                => new RotateVectorMotion(item, From, to);
        }

        public RotateVectorMotion(Transform item, Vector3 value) : base(item, value)
        {
        }

        public RotateVectorMotion(Transform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.eulerAngles;
        protected override void SetValue(Vector3 value) => item.eulerAngles = value;
    }
}
