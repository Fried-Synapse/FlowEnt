using UnityEngine;

namespace FlowEnt.Motions.Transforms
{
    public class MoveLocalToVectorMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public MoveLocalToVectorMotion(TTransform item, Vector3 to) : base(item)
        {
            To = to;
        }

        public MoveLocalToVectorMotion(TTransform item, Vector3 from, Vector3 to) : this(item, to)
        {
            From = from;
        }

        public Vector3? From { get; private set; }
        public Vector3 To { get; }

        public override void OnStart()
        {
            if (From == null)
            {
                From = Item.localPosition;
            }
            else
            {
                Item.localPosition = From.Value;
            }
        }

        public override void OnUpdate(float t)
        {
            Item.localPosition = Vector3.LerpUnclamped(From.Value, To, t);
        }
    }
}
