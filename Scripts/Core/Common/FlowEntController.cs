using System;
using UnityEngine;

namespace FlowEnt
{
    public class FlowEntController : MonoBehaviour
    {
        private const int DefaultMaxArrayCapacity = 6400;

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

        private ulong lastId;
        internal ulong GetId() => lastId++;

        private FastList<AbstractUpdatable> onUpdateCallbacks = new FastList<AbstractUpdatable>(DefaultMaxArrayCapacity);

        private float timeScale = 1;

        public float TimeScale
        {
            get { return timeScale; }
            set
            {

            }
        }

        private PlayState playState = PlayState.Playing;

        public PlayState PlayState { get => playState; }

        private void Update()
        {
            if (playState != PlayState.Playing)
            {
                return;
            }
            for (int i = 0; i < onUpdateCallbacks.Count; ++i)
            {
                onUpdateCallbacks[i].UpdateInternal(Time.deltaTime * timeScale);
            }
        }

        internal void SubscribeToUpdate(AbstractUpdatable updatable)
        {
            onUpdateCallbacks.Add(updatable);
        }

        internal void UnsubscribeFromUpdate(AbstractUpdatable updatable)
        {
            onUpdateCallbacks.Remove(updatable);
        }

        public void Pause()
        {
            playState = PlayState.Paused;
        }

        public void Resume()
        {
            playState = PlayState.Playing;
        }

        #region Options

        public void SetMaxCapacity(int capacity)
        {
            onUpdateCallbacks = new FastList<AbstractUpdatable>(capacity);
        }

        public void SetTimeScale(float timeScale)
        {
            if (timeScale < 0)
            {
                throw new ArgumentException("Value cannot be less than 0");
            }
            this.timeScale = timeScale;
        }

        #endregion
    }
}
