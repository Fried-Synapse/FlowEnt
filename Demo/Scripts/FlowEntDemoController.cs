using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Demo
{
    public class FlowEntDemoController : MonoBehaviour
    {
        [SerializeField]
        private Button replayButton;
        private Button ReplayButton => replayButton;

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

        private void Awake()
        {
            ReplayButton.onClick.AddListener(Replay);
        }

        private void Start()
        {
            new Flow()
                .QueueDelay(1f)
                .Queue(GetInitialLookAround(Objects[0]))
                .At(6f, CameraFlow.GetFlow())
                .QueueDelay(1f)
                .OnCompleted(() => ReplayButton.gameObject.SetActive(true))
                .Start();
        }

        private void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private Flow GetInitialLookAround(Transform transform)
        {
            Vector3 initial = new Vector3(0, 0, 0);
            Vector3 step1 = new Vector3(15, -25, 0);
            Vector3 step2 = new Vector3(25, 25, 0);
            Vector3 step3 = new Vector3(0, 0, 0);

            transform.localEulerAngles = initial;

            return new Flow()
                .Queue(new Tween(1.5f).SetEasing(Easing.EaseInOutSine).For(transform).RotateLocalTo(initial, step1))
                .QueueDelay(0.5f)
                .Queue(new Tween(1.5f).SetEasing(Easing.EaseInOutSine).For(transform).RotateLocalTo(step1, step2))
                .QueueDelay(0.5f)
                .Queue(new Tween(2f).SetEasing(Easing.EaseInOutSine).For(transform).RotateLocalTo(step2, step3));
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