using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Tests.Unit
{
    public class FlowEntStopwatch
    {
        public float Seconds { get; private set; }

        private void Update()
        {
            Seconds += Time.unscaledDeltaTime;
        }

        public void Start()
        {
            EditorApplication.update += Update;
        }

        public void Stop()
        {
            EditorApplication.update -= Update;
        }

        public void Reset()
        {
            Seconds = 0;
        }
    }
}