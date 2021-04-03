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
        public Vector3? From { get; private set; }
        public Vector3? To { get; private set; }

        public override void OnStart()
        {
            From = Item.rotation.eulerAngles;
            To = Item.rotation.eulerAngles + Value.eulerAngles;
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