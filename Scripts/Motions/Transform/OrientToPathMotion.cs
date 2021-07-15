using UnityEngine;

namespace FlowEnt.Motions.TransformMotions
{
    public class OrientToPathMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public OrientToPathMotion(TTransform item) : base(item)
        {
        }

        private Vector3? OldPosition { get; set; }

        public override void OnStart()
        {
            OldPosition = Item.position;
        }

        public override void OnUpdate(float t)
        {
            Vector3 relativePosition = Item.position - OldPosition.Value;
            Item.rotation = Quaternion.LookRotation(relativePosition);
            OldPosition = Item.position;
        }
    }
}