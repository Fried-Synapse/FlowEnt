using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class ScaleVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public ScaleVectorMotion(TTransform item, Vector3 value) : base(item)
        {
            Value = value;
        }

        public Vector3 Value { get; }
        public Vector3? From { get; private set; }
        public Vector3? To { get; private set; }

        public override void OnStart()
        {
            From = Item.localScale;
            To = Vector3.Scale(From.Value, Value);
        }

        public override void OnUpdate(float t)
        {
            Item.localScale = Vector3.Lerp(From.Value, To.Value, t);
        }
    }
}