using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class UpdateTracker : MonoBehaviour
    {
        public Dictionary<UpdateType, List<float>> Values { get; } = new()
        {
            { UpdateType.Update, new List<float>() },
            { UpdateType.SmoothUpdate, new List<float>() },
            { UpdateType.UnscaledUpdate, new List<float>() },
            { UpdateType.LateUpdate, new List<float>() },
            { UpdateType.SmoothLateUpdate, new List<float>() },
            { UpdateType.UnscaledLateUpdate, new List<float>() },
            { UpdateType.FixedUpdate, new List<float>() },
        };

        private void Update()
        {
            Values[UpdateType.Update].Add(Time.deltaTime);
            Values[UpdateType.SmoothUpdate].Add(Time.smoothDeltaTime);
            Values[UpdateType.UnscaledUpdate].Add(Time.unscaledDeltaTime);
        }

        private void LateUpdate()
        {
            Values[UpdateType.LateUpdate].Add(Time.deltaTime);
            Values[UpdateType.SmoothLateUpdate].Add(Time.smoothDeltaTime);
            Values[UpdateType.UnscaledLateUpdate].Add(Time.unscaledDeltaTime);
        }

        private void FixedUpdate()
        {
            Values[UpdateType.FixedUpdate].Add(Time.fixedDeltaTime);
        }
    }
}