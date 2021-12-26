using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Transforms
{
    /// <summary>
    /// Orients the object to the path its taken using the <see cref="Quaternion.LookRotation(Vector3)" /> method.
    /// </summary>
    /// <typeparam name="TTransform"></typeparam>
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