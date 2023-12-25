using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public class SplineValueMotion : AbstractEventMotion<Vector3>
    {
        [Serializable]
        public class Builder : AbstractEventMotionBuilder
        {
            [SerializeField]
            private SplineFactory.SplineType type;
            [SerializeField]
            private bool normalise;
            // [SerializeField]
            // private bool preview;
            [SerializeField]
            private List<Vector3> points = new() { Vector3.zero, Vector3.zero };

            public override ITweenMotion Build()
                => new SplineValueMotion(SplineFactory.GetSpline(type, points, normalise), GetCallback());

#if UNITY_EDITOR
            //TODO this function is not called because this is not a monobehaviour...
            private void OnDrawGizmos()
            {
                // if (preview)
                // {
                //     GetSpline().DrawGizmo(Color.white, 2f);
                // }
            }
#endif
        }
        /// <summary>
        /// Lerps an <see cref="ISpline" />.
        /// </summary>
        public SplineValueMotion(ISpline spline, Action<Vector3> onUpdated) : base(onUpdated)
        {
            this.spline = spline;
        }

        private readonly ISpline spline;

        public override void OnUpdate(float t)
        {
            onUpdated(spline.GetPoint(t));
        }
    }
}