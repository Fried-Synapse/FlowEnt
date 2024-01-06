using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class UpdateTracker : MonoBehaviour
    {
        public Dictionary<UpdateType, List<float>> Values = new()
        {
            { UpdateType.Update, new List<float>() },
            { UpdateType.SmoothUpdate, new List<float>() },
            { UpdateType.LateUpdate, new List<float>() },
            { UpdateType.SmoothLateUpdate, new List<float>() },
            { UpdateType.FixedUpdate, new List<float>() }
        };

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
    }
}
