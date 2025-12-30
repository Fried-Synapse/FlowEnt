using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    public class FlowEntDemoCharacterController : MonoBehaviour
    {
        [SerializeField]
        private EchoBuilder echo;

        private EchoBuilder Echo => echo;

        private void Start()
        {
            InitCharacterAnimation();
        }

        private void InitCharacterAnimation()
        {
            Echo controlEcho = Echo.Build()
                .OnStarting(() => Debug.Log("Initialising character controller."))
                .SetStopCondition(_ => Input.GetKeyDown(KeyCode.Escape))
                .Start();
            new Echo().OnUpdating(_ =>
            {
                if (Input.GetKeyDown(KeyCode.Return) && controlEcho.PlayState == PlayState.Finished)
                {
                    controlEcho.Restart();
                }
            }).Start();
        }
    }
}