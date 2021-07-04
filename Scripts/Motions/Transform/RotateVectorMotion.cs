using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class RotateVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateVectorMotion(TTransform item, Vector3 value) : base(item)
        {
            Value = value;
        }

        public Vector3 Value { get; }
        public Vector3? From { get; private set; }
        public Vector3? To { get; private set; }

        public override void OnStart()
        {
            From = Item.rotation.eulerAngles;
            To = Item.rotation.eulerAngles + Value;
        }

        public override void OnUpdate(float t)
        {
            Item.rotation = Quaternion.Euler(Vector3.LerpUnclamped(From.Value, To.Value, t));
        }

        public override void OnComplete()
        {
        }
    }
}
