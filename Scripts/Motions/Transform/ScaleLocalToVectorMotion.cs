using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class ScaleLocalToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public ScaleLocalToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            To = to;
        }

        public Vector3? From { get; private set; }
        public Vector3 To { get; }

        public override void OnStart()
        {
            From = Item.localScale;
        }

        public override void OnUpdate(float t)
        {
            Item.localScale = Vector3.Lerp(From.Value, To, t);
        }

        public override void OnComplete()
        {
        }
    }
}