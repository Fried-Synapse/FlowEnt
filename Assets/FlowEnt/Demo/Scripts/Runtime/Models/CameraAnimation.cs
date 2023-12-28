using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class CameraAnimation : AbstractDemoAnimation, IEditorDrawer
    {
        private const float Time = 30f;

#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private Transform wrapper;
        private Transform Wrapper => wrapper;

        [SerializeField]
        private Transform transform;
        private Transform Transform => transform;

        [SerializeField]
        private List<Vector3> splinePoints;
        private List<Vector3> SplinePoints => splinePoints;
#pragma warning restore RCS1169, IDE0044

        private ICurve GetSpline() => new BSpline(SplinePoints).Normalise();

        public override AbstractAnimation GetAnimation()
            => new Tween(Time)
                .SetEasing(Easing.EaseInOutSine)
                .For(Wrapper)
                    .MoveTo(GetSpline())
                .For(Transform)
                    .LookAt(new Vector3(0f, 0.5f, 0f));

#if UNITY_EDITOR
        public void OnDraw()
        {
            GetSpline().DrawGizmo(Color.white, 2f);
        }
#endif
    }
}
