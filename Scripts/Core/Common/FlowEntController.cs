using System;
using UnityEngine;

namespace FlowEnt
{
    public class FlowEntController : MonoBehaviour,
        IUpdateController
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

        private FastList<AbstractUpdatable, UpdatableAnchor> updatables = new FastList<AbstractUpdatable, UpdatableAnchor>();

        private float timeScale = 1;

        public float TimeScale
        {
            get { return timeScale; }
            set
            {
                timeScale = value;
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

            float deltaTime = Time.deltaTime * timeScale;
            AbstractUpdatable index = updatables.anchor.next;

            while (index != null)
            {
                index.UpdateInternal(deltaTime);
                index = index.next;
            }
        }

        #region IUpdateController

        void IUpdateController.SubscribeToUpdate(AbstractUpdatable updatable)
        {
            updatables.Add(updatable);
        }

        void IUpdateController.UnsubscribeFromUpdate(AbstractUpdatable updatable)
        {
            updatables.Remove(updatable);
        }

        #endregion

        #region Lifecycle

        public void Pause()
        {
            playState = PlayState.Paused;
        }

        public void Resume()
        {
            playState = PlayState.Playing;
        }

        #endregion

        #region Options

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