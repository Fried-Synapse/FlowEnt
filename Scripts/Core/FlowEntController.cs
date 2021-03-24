using System;
using UnityEngine;

namespace FlowEnt
{
    public class FlowEntController : MonoBehaviour
    {
        private static FlowEntController instance;
        private static object lockObject = new object();

        public static FlowEntController Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        GameObject gameObject = new GameObject("FlowEnt");
                        gameObject.hideFlags = HideFlags.HideInHierarchy;
                        instance = gameObject.AddComponent<FlowEntController>();
                    }
                }
                return instance;
            }
        }

        private PlayState PlayState { get; set; } = PlayState.Playing;

        private Action<float> OnUpdate { get; set; }

        private void Update()
        {
            if (PlayState == PlayState.Playing)
            {
                OnUpdate?.Invoke(Time.deltaTime);
            }
        }

        public void SubscribeToUpdate(Action<float> onUpdate)
            => OnUpdate += onUpdate;

        public void UnsubscribeFromUpdate(Action<float> onUpdate)
            => OnUpdate -= onUpdate;

        public void Pause()
        {
            PlayState = PlayState.Paused;
        }

        public void Resume()
        {
            PlayState = PlayState.Playing;
        }
    }
}
