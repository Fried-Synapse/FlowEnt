using System.Collections.Generic;
using System.Threading.Tasks;
using FriedSynapse.FlowEnt;
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

    [SerializeField]
    private AnimationCurve animationCurve;
    private AnimationCurve AnimationCurve => animationCurve;

    private async void Start()
    {
        await new Tween(1f).StartAsync();

        await BezierFlow(Objects[0]);

        Objects[1].Tween(2)
            .SetTimeScale(0.5f)
            .Move(Vector3.up)
            .RotateY(180f);

        await new Flow(new FlowOptions() { LoopCount = 2, AutoStart = true })
            .SetTimeScale(10)
            .Queue(t => t
                .SetTime(2)
                .For(Objects[2])
                    .MoveY(2)
                .For(Objects[2].GetComponent<MeshRenderer>())
                    .ColorTo(Color.black))
            .Queue(Objects[2].Tween(2f).MoveY(2))
            .Queue(t => t.SetTime(1))
            .At(1, Objects[2].Tween(3f).MoveY(2))
            .Queue(t => t.SetOptions(o => o.SetTime(1)))
            .StartAsync();
    }

    #region Flow

    private async Task BezierFlow(Transform transform)
    {
        BezierSpline spline = new BezierSpline(SplinePoints);
        await transform
            .Tween(5f)
                .MoveTo(spline)
            .OrientToPath()
            .OnCompleted(() => transform.transform.rotation = Quaternion.identity)
            .AsAsync();
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
            //Gizmos.DrawLine(SplinePoints[i], SplinePoints[i + 1]);
        }
#if UNITY_EDITOR
        new BezierSpline(SplinePoints).DrawGizmo(Color.green, 2f);
        new LinearSpline(SplinePoints).DrawGizmo(Color.blue, 2f);
        new BezierCurve(SplinePoints[0], SplinePoints[1], SplinePoints[2], SplinePoints[3]).DrawGizmo(Color.red, 2f);
#endif
    }

    #endregion
}
