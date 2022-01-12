using UnityEngine;

namespace FriedSynapse.FlowEnt.Builder
{
    public class TestScript : MonoBehaviour
    {
        private void Start()
        {
            transform.eulerAngles = Vector3.zero;
            Debug.Log($"start rotation: {transform.eulerAngles}");
        }

        private void Update()
        {
            float time = (Time.time - 1f) * 10f;
            if (time < 0 || time > 1f)
            {
                return;
            }
            Vector3 rotation = transform.eulerAngles;
            float x = Mathf.Lerp(170f, 0, time);
            rotation.x = x;
            transform.eulerAngles = rotation;
            Debug.Log($"rotation: {transform.eulerAngles}, x: {x}");
        }
    }
}
