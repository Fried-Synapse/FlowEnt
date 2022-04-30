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
                lateDeltaTime = Time.deltaTime,
                lateSmoothDeltaTime = Time.smoothDeltaTime,
                fixedDeltaTime = Time.fixedDeltaTime,
            };

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

        private void OnDestroy()
        {
            controller.ResetInstance();
        }
#pragma warning restore IDE0051, RCS1213
    }
}