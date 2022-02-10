using FriedSynapse.FlowEnt.Motions.Echo.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Echo.Rigidbodies
{
    /// <summary>
    /// Rotates the rigidbody using the inputs.
    /// </summary>
    public class RotateByInputMotion<TRigidbody> : AbstractEchoMotion<TRigidbody>
        where TRigidbody : Rigidbody
    {
        public const float DefaultSensitivity = 5f;
        public RotateByInputMotion(TRigidbody item, Transform camera, float sensitivity = DefaultSensitivity) : base(item)
        {
            this.sensitivity = sensitivity;
            this.camera = camera;
            transform = item.transform;
        }

        private readonly float sensitivity;
        private readonly Transform camera;
        private readonly Transform transform;

        public override void OnUpdate(float deltaTime)
        {
            float horizontalRotation = Input.GetAxis("Mouse X");
            float verticalRotation = Input.GetAxis("Mouse Y");

            transform.Rotate(0, horizontalRotation * sensitivity, 0);
            camera.Rotate(-verticalRotation * sensitivity, 0, 0);

            Vector3 currentRotation = camera.localEulerAngles;
            if (currentRotation.x > 180) currentRotation.x -= 360;
            camera.localRotation = Quaternion.Euler(currentRotation);
        }
    }
}