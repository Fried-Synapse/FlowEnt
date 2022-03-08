using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public class FlowEntRuntimeController : MonoBehaviour
    {
        public FlowEntController Controller { get; set; }

#pragma warning disable IDE0051, RCS1213
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            Controller.Update(Time.deltaTime, Time.smoothDeltaTime);
        }

        private void LateUpdate()
        {
            Controller.LateUpdate(Time.deltaTime, Time.smoothDeltaTime);
        }

        private void FixedUpdate()
        {
            Controller.FixedUpdate(Time.fixedDeltaTime);
        }

        public void CustomUpdate(float deltaTime)
        {
            Controller.CustomUpdate(deltaTime);
        }
#pragma warning restore IDE0051, RCS1213
    }
}