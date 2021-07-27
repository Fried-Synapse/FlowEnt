using UnityEngine;

namespace FlowEnt.Motions.Transforms
{
    public class MoveVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveVectorMotion(TTransform item, Vector3 value) : base(item)
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
            Item.position = Vector3.LerpUnclamped(From.Value, To.Value, t);
        }
    }
}
