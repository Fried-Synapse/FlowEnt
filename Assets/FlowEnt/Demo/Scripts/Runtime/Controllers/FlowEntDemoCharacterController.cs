using UnityEngine;

namespace FriedSynapse.FlowEnt.Demo
{
    public class FlowEntDemoCharacterController : MonoBehaviour
    {
#pragma warning disable RCS1169, IDE0044
        [SerializeField]
        private CharacterController characterController;
        private CharacterController CharacterController => characterController;
        [SerializeField]
        private Rigidbody characterRigidBody;
        private Rigidbody CharacterRigidBody => characterRigidBody;
        [SerializeField]
        private new Camera camera;
        private Camera Camera => camera;
#pragma warning restore RCS1169, IDE0044

#pragma warning disable IDE0051, RCS1213
        private void Start()
        {
            InitCharacterAnimation();
        }
#pragma warning restore IDE0051, RCS1213

        private void InitCharacterAnimation()
        {
            Echo controlEcho = CharacterRigidBody
                .Echo()
                    .OnStarting(() => Debug.Log("Initialising character controller."))
                    .SetStopCondition(_ => Input.GetKeyDown(KeyCode.Escape))
                    .MoveByInput()
                    .RotateByInput(Camera.transform)
                    .JumpByInput(20)
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