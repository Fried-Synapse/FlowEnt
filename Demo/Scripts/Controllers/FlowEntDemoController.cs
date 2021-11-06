using System.Collections.Generic;
using System.Linq;
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
        private List<Phase> phases;
        private List<Phase> Phases => phases;

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
                .Queue(GetPhase1(Phases[0]))
                .Queue(GetPhase2(Phases[1]))
                .At(6f, CameraFlow.GetAnimation())
                .QueueDelay(1f)
                .OnCompleted(() => ReplayButton.gameObject.SetActive(true))
                .Start();
        }

        private void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private Flow GetPhase1(Phase phase)
        {
            Vector3 initial = new Vector3(0, 0, 0);
            Vector3 step1 = new Vector3(15, -25, 0);
            Vector3 step2 = new Vector3(25, 25, 0);
            Vector3 step3 = new Vector3(0, 0, 0);

            transform.localEulerAngles = initial;

            Transform character = phase.Objects[0];

            return new Flow()
                .Queue(new Tween(1.5f).SetEasing(Easing.EaseInOutSine).For(character).RotateLocalTo(initial, step1))
                .QueueDelay(0.5f)
                .Queue(new Tween(1.5f).SetEasing(Easing.EaseInOutSine).For(character).RotateLocalTo(step1, step2))
                .QueueDelay(0.5f)
                .Queue(new Tween(2f).SetEasing(Easing.EaseInOutSine).For(character).RotateLocalTo(step2, step3))
                .Queue(new Tween(1f).For(character).ScaleLocalTo(Vector3.one).MoveLocalTo(Vector3.zero));
        }

        private Flow GetPhase2(Phase phase)
        {
            List<Transform> first = phase.Objects.Take(4).ToList();
            List<Transform> second = phase.Objects.Skip(4).Take(4).ToList();
            return new Flow()
                .Queue(new Tween(0.3f).For(first).Apply(t =>
                            {
                                t.OnStarted(() =>
                                {
                                    t.Item.gameObject.SetActive(true);
                                    t.Item.localPosition = new Vector3(t.Item.localPosition.x, -0.5f, t.Item.localPosition.z);
                                });
                                t.ScaleLocalToY(1f).MoveLocalToY(0f);
                            }))
                .Queue(new Tween(2f).SetEasing(new BounceEasing(5)).For(first).Apply(t => t.MoveLocalToY(Random.Range(2f, 5f))))
                .QueueDelay(1.7f)
                .Queue(new Tween(1f).SetEasing(Easing.EaseInCirc).For(first).Apply(t => t.MoveLocalTo(Vector3.zero).OnCompleted(() => t.Item.gameObject.SetActive(false))))
                .At(2f, new Tween(2f).SetEasing(Easing.EaseOutQuad).For(second).Apply(t => t.ScaleLocalTo(Vector3.one).RotateY(720)))
                .Queue(new Tween(1f).SetEasing(Easing.EaseInCirc).For(second).Apply(t => t.MoveLocalTo(Vector3.zero).OnCompleted(() => t.Item.gameObject.SetActive(false))));
        }

        #region Editor

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            CameraFlow.GetSpline().DrawGizmo(Color.white, 2f);
        }
#endif

        #endregion
    }
}