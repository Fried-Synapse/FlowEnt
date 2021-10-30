using System.Collections.Generic;
using System.Threading.Tasks;
using FriedSynapse.FlowEnt;
using UnityEngine;

public class FlowEntDemoController : MonoBehaviour
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
            .Conditional(() => true, flow => flow.Queue(t => t.SetTime(1)))
            .Queue(t => t.SetTime(1))
            .At(1, Objects[2].Tween(3f).MoveY(2))
            .Queue(t => t.SetOptions(o => o.SetTime(1)))
            .StartAsync();
        Animate(null, null);
    }

    #region Flow

    private async Task BezierFlow(Transform transform)
    {
        ISpline spline = new CatmullRomSpline(SplinePoints).Normalise();
        await transform
            .Tween(5f)
            .SetLoopType(LoopType.PingPong)
            .SetLoopCount(4)
                .MoveTo(spline)
            .OrientToPath()
            .OnCompleted(() => transform.transform.rotation = Quaternion.identity)
            .AsAsync();
    }

    private async void Animate(Transform transform, List<Vector3> splinePoints)
    {
        // Tween tween = new Tween(10f)
        //     .SetLoopType(LoopType.PingPong)
        //     .SetLoopCount(4)
        //     .For(transform)
        //         .MoveTo(new CatmullRomSpline(splinePoints).Normalise())
        //         .OrientToPath()
        //     .OnCompleted(() => Debug.Log("Completed!"))
        //     .Start();

        Tween tween = new Tween(1f)
            .For(transform)
            .SetSkipFrames(20)
                .Move(Vector3.one)
            .Start();


        await tween.AsAsync();
    }

    #endregion

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

        new CubicSpline(SplinePoints).DrawGizmo(Color.blue, 2f);
        new BSpline(SplinePoints).DrawGizmo(Color.green, 2f);
        new CatmullRomSpline(SplinePoints).DrawGizmo(Color.yellow, 2f);
        new BezierCurve(SplinePoints[0], SplinePoints[1], SplinePoints[2], SplinePoints[3]).DrawGizmo(Color.red, 2f);
        new LinearSpline(SplinePoints).DrawGizmo(Color.white, 2f);
    }
#endif

    #endregion
}
