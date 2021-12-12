using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// A singleton monobehaviour that is the core of the FlowEnt library. Hold the main update method and a handful of global parameters.
    /// The script automatically attaches itself to an object in the scene called FlowEnt(created by this script).
    /// Make sure not to delete this object because the library will not work without this script attached to an object in the scene.
    /// </summary>
    public class FlowEntController : MonoBehaviour,
        IUpdateController,
        IControllable
    {
        private static FlowEntController instance;
        private static readonly object lockObject = new object();

        /// <summary>
        /// The instance of <see cref="FlowEntController"/>.
        /// </summary>
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

        /// <summary>
        /// Returns weather the <see cref="FlowEntController"/> has an instance or not.
        /// </summary>
        public static bool HasInstance => instance != null;

        private readonly FastList<AbstractUpdatable, UpdatableAnchor> updatables = new FastList<AbstractUpdatable, UpdatableAnchor>();

        private float timeScale = 1f;

        /// <summary>
        /// The global time scale for all animations.
        /// </summary>
        public float TimeScale
        {
            get { return timeScale; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Value cannot be less than 0");
                }
                timeScale = value;
            }
        }

        private PlayState playState = PlayState.Playing;

        /// <summary>
        /// The current state of animations.
        /// </summary>
        public PlayState PlayState { get => playState; }

#pragma warning disable IDE0051, RCS1213
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

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
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
                try
                {
#endif
                index.UpdateInternal(deltaTime);
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
                }
                catch (Exception ex)
                {
                    FlowEntDebug.LogError(
                        $"<color={FlowEntConstants.Red}><b>Exception on update</b></color>\n" +
                        $"<color={FlowEntConstants.Orange}><b>Origin of animation that generated the exception</b></color>:\n" +
                        $"<color={FlowEntConstants.Orange}>{index.stackTrace}</color>\n\n" +
                        $"<b>Exception</b>:\n{ex}");
                }
#endif
                index = index.next;
            }
        }
#pragma warning restore IDE0051, RCS1213

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

        /// <summary>
        /// Pauses all animations.
        /// </summary>
        public void Pause()
        {
            playState = PlayState.Paused;
        }

        /// <summary>
        /// Resumes all animations.
        /// </summary>
        public void Resume()
        {
            playState = PlayState.Playing;
        }

        /// <summary>
        /// Stops all animations.
        /// </summary>
        /// <param name="triggerOnCompleted">If set to true will trigger the "OnCompleted" event on all animations.</param>
        public void Stop(bool triggerOnCompleted = false)
        {
            AbstractUpdatable index = updatables.anchor.next;

            while (index != null)
            {
                AbstractUpdatable temp = index;
                index = index.next;
                temp.Stop(triggerOnCompleted);
            }
        }

        #endregion
    }
}