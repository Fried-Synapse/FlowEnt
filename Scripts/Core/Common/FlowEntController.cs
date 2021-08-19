using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
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

        private FastList<AbstractUpdatable, UpdatableAnchor> playingUpdatables = new FastList<AbstractUpdatable, UpdatableAnchor>();
        private FastList<AbstractUpdatable, UpdatableAnchor> pausedUpdatables = new FastList<AbstractUpdatable, UpdatableAnchor>();

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
            float deltaTime = Time.deltaTime * timeScale;
            AbstractUpdatable index = playingUpdatables.anchor.next;

            while (index != null)
            {
                index.UpdateInternal(deltaTime);
                index = index.next;
            }
        }

        #region IUpdateController

        void IUpdateController.SubscribeToUpdate(AbstractUpdatable updatable)
        {
            playingUpdatables.Add(updatable);
        }

        void IUpdateController.UnsubscribeFromUpdate(AbstractUpdatable updatable)
        {
            playingUpdatables.Remove(updatable);
        }

        #endregion

        #region Lifecycle

        public void Pause()
        {
            FastList<AbstractUpdatable, UpdatableAnchor> temp = playingUpdatables;
            playingUpdatables = pausedUpdatables;
            pausedUpdatables = temp;

            playState = PlayState.Paused;
        }

        public void Resume()
        {
            FastList<AbstractUpdatable, UpdatableAnchor> temp = pausedUpdatables;
            pausedUpdatables = playingUpdatables;
            playingUpdatables = temp;

            playState = PlayState.Playing;
        }

        public void StopAll(bool triggerOnCompleted = false)
        {
            AbstractUpdatable index = (playState == PlayState.Playing ? playingUpdatables : pausedUpdatables).anchor.next;

            while (index != null)
            {
                AbstractUpdatable temp = index;
                index = index.next;
                temp.Stop(triggerOnCompleted);
            }
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