using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class MoveLocalVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalVectorMotion(TTransform item, Vector3 value) : base(item)
        {
            Value = value;
        }

        public Vector3 Value { get; }
        public Vector3? From { get; private set; }
        public Vector3? To { get; private set; }

        public override void OnStart()
        {
            From = Item.position;
            To = From + Value;
        }

        public override void OnUpdate(float t)
        {
            Item.localPosition = Vector3.LerpUnclamped(From.Value, To.Value, t);
        }
    }
}