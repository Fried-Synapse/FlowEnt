using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    public class FlowEntDemoCharacterController : MonoBehaviour
    {
#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private EchoBuilder echo;
        private EchoBuilder Echo => echo;

#pragma warning restore RCS1169, IDE0044

#pragma warning disable IDE0051, RCS1213
        private void Start()
        {
            InitCharacterAnimation();
        }
#pragma warning restore IDE0051, RCS1213

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
                    controlEcho.Reset().Start();
                }
            }).Start();
        }
    }
}