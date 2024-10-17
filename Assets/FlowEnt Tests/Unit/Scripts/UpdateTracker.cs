using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class UpdateTracker : MonoBehaviour
    {
        private float guiTime;
        private float guiDeltaTime;

        public Dictionary<UpdateType, List<float>> Values { get; } = new()
        {
            { UpdateType.Update, new List<float>() },
            { UpdateType.SmoothUpdate, new List<float>() },
            { UpdateType.LateUpdate, new List<float>() },
            { UpdateType.SmoothLateUpdate, new List<float>() },
            { UpdateType.FixedUpdate, new List<float>() },
            { UpdateType.GuiUpdate, new List<float>() }
        };

        private void Awake()
        {
            guiTime = Time.realtimeSinceStartup;
        }

        private void Update()
        {
            Values[UpdateType.Update].Add(Time.deltaTime);
            Values[UpdateType.SmoothUpdate].Add(Time.smoothDeltaTime);
        }

        private void LateUpdate()
        {
            Values[UpdateType.LateUpdate].Add(Time.deltaTime);
            Values[UpdateType.SmoothLateUpdate].Add(Time.smoothDeltaTime);
        }

        private void FixedUpdate()
        {
            Values[UpdateType.FixedUpdate].Add(Time.fixedDeltaTime);
        }

        private void OnGUI()
        {
            guiDeltaTime = Time.realtimeSinceStartup - guiTime;
            Values[UpdateType.GuiUpdate].Add(guiDeltaTime);
            guiTime = Time.realtimeSinceStartup;
        }
    }
}