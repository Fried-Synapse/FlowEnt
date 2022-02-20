using System;
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
    public class MoveVectorValueMotionBuilder : AbstractTweenMotionBuilder<Transform>
    {
        [SerializeField]
        private Vector3 value;

        public override ITweenMotion Build()
        {
            return new MoveVectorMotion(item, value);
        }
    }

    [Serializable]
    public class MoveVectorFromToMotionBuilder : AbstractTweenMotionBuilder<Transform>
    {
        [SerializeField]
        private Vector3 from;
        [SerializeField]
        private Vector3 to;

        public override ITweenMotion Build()
        {
            return new MoveVectorMotion(item, from, to);
        }
    }
}
