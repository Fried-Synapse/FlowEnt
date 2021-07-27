using UnityEngine;

namespace FlowEnt.Motions.Transforms
{
    public class MoveToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            To = to;
        }

        public MoveToVectorMotion(TTransform item, Vector3 from, Vector3 to) : this(item, to)
        {
            From = from;
        }

        public Vector3? From { get; private set; }
        public Vector3 To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.position;
            }
            else
            {
                Item.position = From.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.position = Vector3.LerpUnclamped(From.Value, To, t);
        }
    }
}
