using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Runtime updater for the <see cref="FlowEntController" />.
    /// </summary>
    public class FlowEntRuntimeUpdater : MonoBehaviour, IFlowEntUpdater
    {
        private FlowEntController controller;

        public void SetController(FlowEntController controller)
        {
            this.controller = controller;
        }

#pragma warning disable IDE0051, RCS1213
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            controller.Update(Time.deltaTime, Time.smoothDeltaTime);
        }

        private void LateUpdate()
        {
            controller.LateUpdate(Time.deltaTime, Time.smoothDeltaTime);
        }

        private void FixedUpdate()
        {
            controller.FixedUpdate(Time.fixedDeltaTime);
        }

        public void CustomUpdate(float deltaTime)
        {
            controller.CustomUpdate(deltaTime);
        }
#pragma warning restore IDE0051, RCS1213
    }
}