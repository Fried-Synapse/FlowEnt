using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class RotateToQuaternionMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateToQuaternionMotion(TTransform item, Quaternion to) : base(item)
        {
            To = to;
        }

        public RotateToQuaternionMotion(TTransform item, Quaternion from, Quaternion to) : this(item, to)
        {
            From = from;
            To = to;
        }

        public Quaternion? From { get; private set; }
        public Quaternion To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.rotation;
            }
            else
            {
                Item.rotation = From.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.rotation = Quaternion.LerpUnclamped(From.Value, To, t);
        }

        public override void OnComplete()
        {
        }
    }
}