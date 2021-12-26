using System.Collections;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
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
            from = item.eulerAngles;
            to = item.eulerAngles + value;
        }

        public override void OnUpdate(float t)
        {
            item.eulerAngles = Vector3.LerpUnclamped(from, to, t);
        }
    }
}
