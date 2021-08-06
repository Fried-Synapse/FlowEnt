using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowEnt.Motions.Transforms
{
    public class RotateVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateVectorMotion(TTransform item, Vector3 value) : base(item)
        {
            this.value = value;
        }

        private readonly Vector3 value;
        private Vector3 from;
        private Vector3 to;

        public override void OnStart()
        {
            from = item.rotation.eulerAngles;
            to = item.rotation.eulerAngles + value;
        }

        public override void OnUpdate(float t)
        {
            item.rotation = Quaternion.Euler(Vector3.LerpUnclamped(from, to, t));
        }
    }
}
