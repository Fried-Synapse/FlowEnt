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

            return new Flow()
                .Queue(new Tween(1.5f).SetEasing(Easing.EaseInOutSine).For(phase.Objects[0]).RotateLocalTo(initial, step1))
                .QueueDelay(0.5f)
                .Queue(new Tween(1.5f).SetEasing(Easing.EaseInOutSine).For(phase.Objects[0]).RotateLocalTo(step1, step2))
                .QueueDelay(0.5f)
                .Queue(new Tween(2f).SetEasing(Easing.EaseInOutSine).For(phase.Objects[0]).RotateLocalTo(step2, step3));
        }

        private Flow GetPhase2(Phase phase)
        {
            return new Flow()
                .Queue(new Tween(0.3f).For(phase.Objects).Apply(t =>
                                                            {
                                                                t.OnStarted(() =>
                                                                {
                                                                    t.Item.gameObject.SetActive(true);
                                                                    t.Item.localPosition = new Vector3(t.Item.localPosition.x, -0.5f, t.Item.localPosition.z);
                                                                });
                                                                t.ScaleLocalToY(1f).MoveLocalToY(0f);
                                                            }))
                .Queue(new Tween(2f).SetEasing(new BounceEasing(4)).For(phase.Objects).Apply(t => t.MoveLocalToY(Random.Range(2f, 5f))));
        }

        #region Editor



        [SerializeField]
        private int bounces;

        [Range(0f, 1f)]
        [SerializeField]
        private float bounciness;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            new BounceEasing(bounces, bounciness).DrawGizmo(Color.green, 2f);
            CameraFlow.GetSpline().DrawGizmo(Color.white, 2f);
        }
#endif

        #endregion
    }
}