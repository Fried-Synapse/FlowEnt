using System.Collections.Generic;
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
        //await BezierFlow(Objects[0]);

        Objects[1].transform.Tween(2f).Move(Vector3.one).RotateY(180f);

        await new Flow(new FlowOptions() { LoopCount = 2 })
            .Queue(t => t
                .SetTime(2)
                .For(Objects[2])
                    .MoveY(2)
                .For(Objects[2].GetComponent<MeshRenderer>())
                    .ColorTo(Color.black))
            .Queue(new TweenOptions() { Time = 2f }, t => t.For(Objects[2]).MoveY(2))
            .Queue(t => t.SetTime(1))
            .At(1, new TweenOptions() { Time = 2f }, t => t.For(Objects[3]).MoveY(2))
            .StartAsync();
    }

    #region Flow

    private async Task BezierFlow(Transform transform)
    {
        await transform
            .Tween(5f)
                .MoveTo(new BezierSpline(SplinePoints))
                .OrientToPath()
            .Tween
            .OnComplete(() =>
            {
                transform.transform.rotation = Quaternion.identity;
            }).AsAsync();
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
