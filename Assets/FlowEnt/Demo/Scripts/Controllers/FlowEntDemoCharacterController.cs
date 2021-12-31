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
        private new Camera camera;
        private Camera Camera => camera;
#pragma warning restore RCS1169, IDE0044

        private void Start()
        {
            CharacterController
                .Echo()
                    .Move(10f)
                    .Rotate(Camera.transform, 5f)
                .Start();
        }
    }
}