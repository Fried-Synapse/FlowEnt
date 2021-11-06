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
        private PhaseXAnimation phaseXAnimation;
        private PhaseXAnimation PhaseXAnimation => phaseXAnimation;

        private void Awake()
        {
            ReplayButton.onClick.AddListener(Replay);
        }

        private void Start()
        {
            new Flow()
                .At(1f, CharacterAnimation.GetAnimation())
                .At(6f, CameraAnimation.GetAnimation())
                .At(6f, Phase1Animation.GetAnimation())
                .At(11f, Phase2Animation.GetAnimation())
                .At(15f, Phase3Animation.GetAnimation())
                .At(35f, PhaseXAnimation.GetAnimation())
                .QueueDelay(3f)
                .OnCompleted(() => ReplayButton.gameObject.SetActive(true))
                .Start();
        }

        private void Replay()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        #region Editor

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            CameraAnimation.OnDraw();
            CharacterAnimation.OnDraw();
        }
#endif

        #endregion
    }
}