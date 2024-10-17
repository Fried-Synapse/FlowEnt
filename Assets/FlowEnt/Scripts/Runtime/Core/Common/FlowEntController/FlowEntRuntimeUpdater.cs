using System;
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
                guiDeltaTime = FlowEntTime.guiDeltaTime,
            };

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

        private void OnGUI()
        {
            FlowEntTime.guiDeltaTime = Time.realtimeSinceStartup - FlowEntTime.guiTime;
            controller.OnGui(FlowEntTime.guiDeltaTime);
            FlowEntTime.guiTime = Time.realtimeSinceStartup;
        }

        private void OnDestroy()
        {
            controller.ResetInstance();
        }
    }
}