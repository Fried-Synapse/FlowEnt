using System.Collections.Generic;
using System.Threading.Tasks;
using FlowEnt;
using UnityEngine;

public class FlowEntExampleController : MonoBehaviour
{
    [SerializeField]
    private List<Transform> cubes;
    private List<Transform> Cubes => cubes;

    [SerializeField]
    private List<Vector3> splinePoints;
    private List<Vector3> SplinePoints => splinePoints;

    private void Start()
    {
        Cubes[0].MoveTo(new Vector3(-2, 2, 0), 1f);

        Flow flow = Flow.Create()
                .Enqueue(2f)
                .Enqueue(5f)
                    .For(Cubes[0])
                    .MoveY(5)
                    .Wrap()
                .Enqueue(3f)
                    .For(Cubes[0])
                        .MoveY(-2)
                    .Wrap()
            .Concurrent()
                .Enqueue(4f)
                    .SetEase(Easing.EaseInBounce)
                    .Loop(LoopType.PingPong, 2)
                    .For(Cubes[1])
                        .MoveAndRotateTo(new Vector3(0, 3, 0), new Vector3(0, 0, 90))
                    .For(Cubes[1].GetComponent<MeshRenderer>())
                        .Alpha(1f, 0f)
                    .Wrap()
            .Concurrent()
                .Enqueue(5f)
                    .For(Cubes[2])
                        //.Move(new LinearSpline(SplinePoints))
                        .Move(new BezierSpline(SplinePoints))
                        .OrientToPath()
                        .OnComplete(() =>
                        {
                            Cubes[2].transform.rotation = Quaternion.identity;
                        })
                    .Wrap()
                .Enqueue(2f)
                    .SetEase(Easing.EaseOutBounce)
                    .For(Cubes[2])
                        .MoveTo(new Vector3(2, 0, 0))
                    .Wrap()
            .Play(-2);

        //Flow.Create()
        //        .Enqueue()
        //            .For(2f)
        //            .OnComplete(() =>
        //            {
        //                flow.Pause();
        //            })
        //        .Enqueue()
        //            .For(2f)
        //            .OnComplete(() =>
        //            {
        //                flow.Resume();
        //            })
        //        .Enqueue()
        //            .For(2f)
        //            .OnComplete(() =>
        //            {
        //                flow.Pause();
        //            })
        //        .Enqueue()
        //            .For(2f)
        //            .OnComplete(() =>
        //            {
        //                flow.Resume();
        //            })
        //        .Play();
    }

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
}
