using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    public class FlowEntDemoController : MonoBehaviour
    {
        [SerializeField]
        private CameraFlow cameraFlow;
        private CameraFlow CameraFlow => cameraFlow;

        [SerializeField]
        private List<Transform> objects;
        private List<Transform> Objects => objects;

        [SerializeField]
        private List<Vector3> splinePoints;
        private List<Vector3> SplinePoints => splinePoints;

        [SerializeField]
        private AnimationCurve animationCurve;
        private AnimationCurve AnimationCurve => animationCurve;

        private void Start()
        {
            CameraFlow.GetFlow().Start();
        }

        #region Editor

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            DrawSplines();
        }

        private void DrawSplines()
        {
            if (SplinePoints.Count < 1)
            {
                return;
            }
            CameraFlow.GetSpline().DrawGizmo(Color.white, 2f);
        }
#endif

        #endregion
    }
}