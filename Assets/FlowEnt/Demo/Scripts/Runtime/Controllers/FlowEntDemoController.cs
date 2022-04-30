using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FriedSynapse.FlowEnt.Demo
{
    public class FlowEntDemoController : MonoBehaviour
    {
#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private Button replayButton;
        private Button ReplayButton => replayButton;

        [SerializeField]
        private Light worldLight;
        private Light WorldLight => worldLight;

        [SerializeField]
        private Transform ground;
        private Transform Ground => ground;

        [SerializeField]
        private CameraAnimation cameraAnimation;
        private CameraAnimation CameraAnimation => cameraAnimation;

        [SerializeField]
        private CharacterAnimation characterAnimation;
        private CharacterAnimation CharacterAnimation => characterAnimation;

        [SerializeField]
        private Phase1Animation phase1Animation;
        private Phase1Animation Phase1Animation => phase1Animation;

        [SerializeField]
        private Phase2Animation phase2Animation;
        private Phase2Animation Phase2Animation => phase2Animation;

        [SerializeField]
        private Phase3Animation phase3Animation;
        private Phase3Animation Phase3Animation => phase3Animation;

        [SerializeField]
        private Phase4Animation phase4Animation;
        private Phase4Animation Phase4Animation => phase4Animation;

        [SerializeField]
        private Phase5Animation phase5Animation;
        private Phase5Animation Phase5Animation => phase5Animation;
#pragma warning restore RCS1169, IDE0044

#pragma warning disable IDE0051, RCS1213
        private void Awake()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
            WebGLInput.captureAllKeyboardInput = false;
#endif
            ReplayButton.onClick.AddListener(Replay);
        }
        public AbstractAnimation Test => CharacterAnimation.Test;
        private void Start()
        {
            new Flow()
                .At(1f, CharacterAnimation.GetAnimation())
                .At(6f, CameraAnimation.GetAnimation())
                .At(6f, Phase1Animation.GetAnimation())
                .At(11f, Phase2Animation.GetAnimation())
                .At(16.5f, Phase3Animation.GetAnimation())
                .At(27f, Phase4Animation.GetAnimation())
                .At(32f, TurnOffLights())
                .At(36f, Phase5Animation.GetAnimation())
                .QueueDelay(3f)
                .OnCompleted(() => ReplayButton.gameObject.SetActive(true))
                .Start();
        }
#pragma warning restore IDE0051, RCS1213

        private AbstractAnimation TurnOffLights() =>
            new Tween(4f).SetEasing(Easing.EaseInOutSine)
                .For(WorldLight).IntensityTo(0f)
                .For(Ground.GetComponent<MeshRenderer>().material).ColorTo("_EmissionColor", Color.clear);

        private void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        #region Editor

#if UNITY_EDITOR
#pragma warning disable IDE0051, RCS1213
        private void OnDrawGizmos()
        {
            CameraAnimation.OnDraw();
            CharacterAnimation.OnDraw();
        }
#pragma warning restore IDE0051, RCS1213
#endif

        #endregion
    }
}