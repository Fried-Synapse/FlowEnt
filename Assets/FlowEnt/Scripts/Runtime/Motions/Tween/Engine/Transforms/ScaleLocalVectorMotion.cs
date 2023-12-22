using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localScale" /> value.
    /// </summary>
    public class ScaleLocalVectorMotion : AbstractVector3Motion<Transform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override ITweenMotion Build()
                => new ScaleLocalVectorMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override ITweenMotion Build()
                => new ScaleLocalVectorMotion(item, From, to);
        }

        public ScaleLocalVectorMotion(Transform item, Vector3 value) : base(item, value)
        {
        }

        public ScaleLocalVectorMotion(Transform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.localScale;
        protected override Vector3 GetTo(Vector3 from, Vector3 value) => Vector3.Scale(from, value);
        protected override void SetValue(Vector3 value) => item.localScale = value;
    }
}