using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.localPosition" /> value.
    /// </summary>
    public class MoveLocalVectorMotion : AbstractVector3Motion<Transform>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveLocalVectorMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new MoveLocalVectorMotion(item, From, to);
        }

        public MoveLocalVectorMotion(Transform item, Vector3 value) : base(item, value)
        {
        }

        public MoveLocalVectorMotion(Transform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.localPosition;
        protected override void SetValue(Vector3 value) => item.localPosition = value;
    }
}