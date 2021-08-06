using UnityEngine;

namespace FlowEnt.Motions.Transforms
{
    public class RotateQuaternionMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public RotateQuaternionMotion(TTransform item, Quaternion value) : base(item)
        {
            this.value = value;
        }

        private readonly Quaternion value;
        private Quaternion from;
        private Quaternion to;

        public override void OnStart()
        {
            from = item.rotation;
            to = item.rotation * value;
        }

        public override void OnUpdate(float t)
        {
            item.rotation = Quaternion.LerpUnclamped(from, to, t);
        }
    }
}