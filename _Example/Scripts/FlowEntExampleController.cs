using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlowEnt;
using UnityEngine;

public class FlowEntExampleController : MonoBehaviour
{
    private const float CameraJourneyLength = 6f;

    [SerializeField]
    private Transform cameraTransform;
    private Transform CameraTransform => cameraTransform;

    [SerializeField]
    private List<Transform> objects;
    private List<Transform> Objects => objects;

    [SerializeField]
    private List<Vector3> splinePoints;
    private List<Vector3> SplinePoints => splinePoints;

    private async void Start()
    {
        Objects[0].transform.Tween(2f).Move(Vector3.one);
        //BezierFlow(Objects[0]);
    }

    #region Flow

    private void BezierFlow(Transform transform)
    {
        transform
            .Tween(5f)
                .MoveTo(new BezierSpline(SplinePoints))
                .OrientToPath()
            .Tween
            .OnComplete(() =>
            {
                transform.transform.rotation = Quaternion.identity;
            });
    }

    #endregion

    #region Editor

    private void OnDrawGizmos()
    {
        DrawSpline();
    }

    private void DrawSpline()
    {
        if (SplinePoints.Count < 1)
        {
            return;
        }
        for (int i = 0; i < SplinePoints.Count - 1; i++)
        {
            Gizmos.DrawLine(SplinePoints[i], SplinePoints[i + 1]);
        }
    }

    #endregion
}
