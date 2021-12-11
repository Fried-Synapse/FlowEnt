using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    [Serializable]
    public class MoveVectorMotionBuilder : AbstractMotionBuilder<Transform>
    {
        [SerializeField]
        private Vector3 value;
        public Vector3 Value => value;

        public override IMotion Build()
        {
            return new MoveVectorMotion<Transform>(item, value);
        }
    }

    public class MoveVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveVectorMotion(TTransform item, Vector3 value) : base(item)
        {
            this.value = value;
        }

        private readonly Vector3 value;
        private Vector3 from;
        private Vector3 to;

        public override void OnStart()
        {
            from = item.position;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.position = Vector3.LerpUnclamped(from, to, t);
        }
    }
}
