using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Runtime updater for the <see cref="FlowEntController" />.
    /// </summary>
    public class FlowEntRuntimeUpdater : MonoBehaviour, IFlowEntUpdater
    {
        private FlowEntController controller;

        void IFlowEntUpdater.SetController(FlowEntController controller)
        {
            this.controller = controller;
        }

        DeltaTimes IFlowEntUpdater.GetDeltaTimes()
            => new DeltaTimes()
            {
                deltaTime = Time.deltaTime,
                smoothDeltaTime = Time.smoothDeltaTime,
                unscaledDeltaTime = Time.unscaledDeltaTime,
                fixedDeltaTime = Time.fixedDeltaTime,
            };

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            controller.Update(Time.deltaTime, Time.smoothDeltaTime, Time.unscaledDeltaTime);
        }

        private void LateUpdate()
        {
            controller.LateUpdate(Time.deltaTime, Time.smoothDeltaTime, Time.unscaledDeltaTime);
        }

        private void FixedUpdate()
        {
            controller.FixedUpdate(Time.fixedDeltaTime);
        }

        private void OnDestroy()
        {
            controller.ResetInstance();
        }
    }
}