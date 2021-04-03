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
        BezierFlow(Objects[0]);

        await CameraFlowAsync();

        ComplexFlow(Objects[1], Objects[2]);

        await Flow.Create().Enqueue(2).PlayAsync();

        Objects[3].MoveZ(4f, 2f);

        ScaleFlow(Objects[4]);

        ColorFlow(Objects[5].GetComponent<MeshRenderer>());
    }

    #region Flow

    private void BezierFlow(Transform transform)
    {
        transform.MoveTo(new BezierSpline(SplinePoints), 5f)
            .OrientToPath()
            .OnComplete(() =>
            {
                transform.transform.rotation = Quaternion.identity;
            });
    }

    private async Task CameraFlowAsync()
    {
        await Flow.Create()
            .Enqueue(3f)
            .SetEase(Easing.EaseInOutCubic)
                .For(CameraTransform)
                    .RotateX(-10f)
                .Tween
            .Enqueue(2f)
                .SetEase(Easing.EaseInOutCubic)
                .For(CameraTransform)
                    .RotateTo(new Vector3(0f, 10f, 0f))
                .Flow
            .PlayAsync();

        CameraTransform.MoveX(CameraJourneyLength, CameraJourneyLength);
    }

    private void ComplexFlow(Transform transform1, Transform transform2)
    {
        Flow.Create()
            .Enqueue(4f)
                .SetEase(Easing.EaseInBounce)
                .Loop(LoopType.PingPong, 2)
                .For(transform1)
                    .MoveAndRotateTo(new Vector3(2, 3, 0), new Vector3(0, 0, 90))
                .For(transform1.GetComponent<MeshRenderer>())
                    .Alpha(1f, 0f)
                .Tween
            .Concurrent()
                .Enqueue(2f)
                .Enqueue(5f)
                    .For(transform2)
                        .MoveY(5)
                    .Tween
                .Enqueue(3f)
                    .For(transform2)
                        .MoveY(-2)
                    .Flow
            .Play(-1);
    }

    private void ScaleFlow(Transform transform)
    {
        Flow.Create()
            .Enqueue(2f)
                .SetEase(Easing.EaseInElastic)
                .For(transform)
                    .Scale(Vector3.one * 3)
                .Tween
            .Enqueue(1.5f)
                .SetEase(Easing.EaseInCubic)
                .For(transform)
                    .ScaleX(1 / 3f)
                .Tween
            .Enqueue(1.5f)
                .SetEase(Easing.EaseInCubic)
                .For(transform)
                    .ScaleLocalTo(Vector3.one)
                .Tween
            .Play();
    }

    private void ColorFlow(MeshRenderer renderer)
    {
        Color color = renderer.material.color;
        Flow.Create()
            .Enqueue(2f)
            .Enqueue(2f)
                .For(renderer)
                    .Color(Color.blue)
                .Tween
            .Enqueue(2f)
                .For(renderer)
                    .Color(Color.cyan, color)
            .Flow
            .Play();
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
