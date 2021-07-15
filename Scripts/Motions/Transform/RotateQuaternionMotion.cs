using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class RotateQuaternionMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateQuaternionMotion(TTransform item, Quaternion value) : base(item)
        {
            Value = value;
        }

        public Quaternion Value { get; }
        public Quaternion? From { get; private set; }
        public Quaternion? To { get; private set; }

        public override void OnStart()
        {
            From = Item.rotation;
            To = Item.rotation * Value;
        }

        public override void OnUpdate(float t)
        {
            Item.rotation = Quaternion.LerpUnclamped(From.Value, To.Value, t);
        }
    }
}