using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    public class OrientToPathMotion<TTransform> : AbstractMotion<TTransform>
        where TTransform : Transform
    {
        public OrientToPathMotion(TTransform item) : base(item)
        {
        }

        private Vector3 oldPosition;

        public override void OnStart()
        {
            oldPosition = item.position;
        }

        public override void OnUpdate(float t)
        {
            Vector3 relativePosition = item.position - oldPosition;
            item.rotation = Quaternion.LookRotation(relativePosition);
            oldPosition = item.position;
        }
    }
}