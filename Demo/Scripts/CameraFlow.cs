using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    [Serializable]
    public class CameraFlow
    {
        [SerializeField]
        private Transform wrapper;
        private Transform Wrapper => wrapper;

        [SerializeField]
        private Transform transform;
        private Transform Transform => transform;

        [SerializeField]
        private List<Vector3> splinePoints;
        private List<Vector3> SplinePoints => splinePoints;

        public ISpline GetSpline() => new BSpline(SplinePoints);

        public Flow GetFlow()
        {
            const float time1 = 30f;
            return new Flow()
                .Queue(new Tween(time1)
                            .SetEasing(Easing.EaseInOutSine)
                            .For(Wrapper)
                                .MoveTo(GetSpline())
                                .OrientToPath()
                            .For(Transform)
                                .LookAt(new Vector3(0f, 0.5f, 0f)));
        }
    }
}
