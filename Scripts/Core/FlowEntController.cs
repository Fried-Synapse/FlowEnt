using System;
using UnityEngine;

namespace FlowEnt
{
    public interface IUpdatable
    {
        public int UpdateIndex { get; set; }
        public void Update(float deltaTime);
    }

    public class FlowEntController : MonoBehaviour
    {
        private const int MaxArrayCapacity = 1000;

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

        private IUpdatable[] onUpdateCallbacks = new IUpdatable[MaxArrayCapacity];
        private int arraySize;
        private int arraySizeOnPause;

        public PlayState PlayState { get; private set; } = PlayState.Playing;

        private void Update()
        {
            for (int i = 0; i < arraySize; i++)
            {
                onUpdateCallbacks[i].Update(Time.deltaTime);
            }
        }

        public void SubscribeToUpdate(IUpdatable updatable)
        {
            onUpdateCallbacks[arraySize] = updatable;
            updatable.UpdateIndex = arraySize;
            arraySize++;
        }

        public void UnsubscribeFromUpdate(IUpdatable updatable)
        {
            int indexToRemove = updatable.UpdateIndex;
            IUpdatable lastUpdatable = onUpdateCallbacks[arraySize - 1];
            onUpdateCallbacks[indexToRemove] = lastUpdatable;
            lastUpdatable.UpdateIndex = indexToRemove;
            arraySize--;
        }

        public void Pause()
        {
            arraySizeOnPause = arraySize;
            arraySize = 0;
            PlayState = PlayState.Paused;
        }

        public void Resume()
        {
            arraySize = arraySizeOnPause;
            PlayState = PlayState.Playing;
        }

        public void SetMaxCapacity(int capacity)
        {
            onUpdateCallbacks = new IUpdatable[capacity];
        }
    }
}
