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
        public ulong GetId() => lastId++;

        private FastList<AbstractUpdatable> onUpdateCallbacks = new FastList<AbstractUpdatable>(DefaultMaxArrayCapacity);

        private PlayState playState = PlayState.Playing;
        public PlayState PlayState { get => playState; }

        private void Update()
        {
            if (playState != PlayState.Playing)
            {
                return;
            }
            for (int i = 0; i < onUpdateCallbacks.Count; i++)
            {
                onUpdateCallbacks[i].UpdateInternal(Time.deltaTime);
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

        public void SetMaxCapacity(int capacity)
        {
            onUpdateCallbacks = new FastList<AbstractUpdatable>(capacity);
        }
    }
}
