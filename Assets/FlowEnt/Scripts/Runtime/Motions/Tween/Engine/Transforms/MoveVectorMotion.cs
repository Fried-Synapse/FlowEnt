using System;
using System.ComponentModel;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Lerps the <see cref="Transform.position" /> value.
    /// </summary>
    public class MoveVectorMotion : AbstractVector3Motion<Transform>
    {
        public MoveVectorMotion(Transform item, Vector3 value) : base(item, value)
        {
        }

        public MoveVectorMotion(Transform item, Vector3? from, Vector3 to) : base(item, from, to)
        {
        }

        protected override Vector3 GetFrom() => item.position;
        protected override void SetValue(Vector3 value) => item.position = value;
    }

    [Serializable]
    public class MoveVectorValueMotionBuilder : AbstractVector3ValueMotionBuilder<Transform>
    {
        public override ITweenMotion Build()
            => new MoveVectorMotion(item, value);
    }

    [Serializable]
    public class MoveVectorFromToMotionBuilder : AbstractVector3FromToMotionBuilder<Transform>
    {
        public override ITweenMotion Build()
            => new MoveVectorMotion(item, from, to);
    }
}
