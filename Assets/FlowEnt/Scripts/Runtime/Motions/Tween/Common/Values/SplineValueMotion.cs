using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.Values
{
    public class SplineValueMotion : AbstractTweenMotion
    {
        [Serializable]
        public class Builder : AbstractValueMotion<Vector3>.AbstractEventBuilder
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private SplineFactory.SplineType type;
            [SerializeField]
            private bool normalise;
            // [SerializeField]
            // private bool preview;
            [SerializeField]
            private List<Vector3> points;
#pragma warning restore IDE0044, RCS1169

            public override ITweenMotion Build()
                => new SplineValueMotion(SplineFactory.GetSpline(type, points, normalise), GetCallback());

#if UNITY_EDITOR
#pragma warning disable IDE0051, RCS1213
            //TODO this function is not called because this is not a monobehaviour...
            private void OnDrawGizmos()
            {
                // if (preview)
                // {
                //     GetSpline().DrawGizmo(Color.white, 2f);
                // }
            }
#pragma warning restore IDE0051, RCS1213
#endif
        }
        /// <summary>
        /// Lerps an <see cref="ISpline" />.
        /// </summary>
        public SplineValueMotion(ISpline spline, Action<Vector3> callback)
        {
            this.spline = spline;
            this.callback = callback;
        }

        private readonly ISpline spline;
        private readonly Action<Vector3> callback;

        public override void OnUpdate(float t)
        {
            callback(spline.GetPoint(t));
        }
    }
}