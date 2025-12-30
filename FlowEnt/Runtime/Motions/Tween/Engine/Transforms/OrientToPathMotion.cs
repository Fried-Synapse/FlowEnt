using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Transforms
{
    /// <summary>
    /// Orients the object to the path its taken using the <see cref="Quaternion.LookRotation(Vector3)" /> method.
    /// </summary>
    public class OrientToPathMotion : AbstractTweenMotion<Transform>
    {
        [Serializable]
        public class Builder : AbstractBuilder
        {
            public override AbstractTweenMotion Build()
                => new OrientToPathMotion(item);
        }

        public OrientToPathMotion(Transform item) : base(item)
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