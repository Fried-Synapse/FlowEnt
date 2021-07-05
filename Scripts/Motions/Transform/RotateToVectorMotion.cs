using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class RotateToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            To = to;
        }

        public RotateToVectorMotion(TTransform item, Vector3 from, Vector3 to) : this(item, to)
        {
            From = from;
        }

        public Vector3? From { get; private set; }
        public Vector3 To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.rotation.eulerAngles;
            }
            else
            {
                Item.rotation = Quaternion.Euler(From.Value);
            }
        }

        public override void OnUpdate(float t)
        {
            Item.rotation = Quaternion.Euler(Vector3.LerpUnclamped(From.Value, To, t));
        }

        public override void OnComplete()
        {
        }
    }
}
