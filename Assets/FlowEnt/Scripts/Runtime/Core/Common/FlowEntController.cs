using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// A singleton monobehaviour that is the core of the FlowEnt library. Hold the main update method and a handful of global parameters.
    /// The script automatically attaches itself to an object in the scene called FlowEnt(created by this script).
    /// Make sure not to delete this object because the library will not work without this script attached to an object in the scene.
    /// </summary>
    [ExecuteInEditMode]
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

        private static IUpdateController updateControllerInstance;
        internal static IUpdateController UpdateControllerInstance
        {
            get
            {
                lock (lockObject)
                {
                    if (updateControllerInstance == null)
                    {
#if UNITY_EDITOR
#pragma warning disable IDE0004 //HACK unity is dum-dum. Doesn't know how to cast.
                        updateControllerInstance = Application.isPlaying ? (IUpdateController)Instance : Editor.FlowEntEditorController.Instance;
#pragma warning restore IDE0004
#else
                        updateControllerInstance = Instance;
#endif
                    }
                }
                return updateControllerInstance;
            }
        }

        /// <summary>
        /// Returns weather the <see cref="FlowEntController"/> has an instance or not.
        /// </summary>
        public static bool HasInstance => instance != null;

        private readonly UpdatablesFastList<AbstractUpdatable> updatables = new UpdatablesFastList<AbstractUpdatable>();
        private readonly UpdatablesFastList<AbstractUpdatable> smoothUpdatables = new UpdatablesFastList<AbstractUpdatable>();
        private readonly UpdatablesFastList<AbstractUpdatable> lateUpdatables = new UpdatablesFastList<AbstractUpdatable>();
        private readonly UpdatablesFastList<AbstractUpdatable> smoothLateUpdatables = new UpdatablesFastList<AbstractUpdatable>();
        private readonly UpdatablesFastList<AbstractUpdatable> fixedUpdatables = new UpdatablesFastList<AbstractUpdatable>();
        private readonly UpdatablesFastList<AbstractUpdatable> customUpdatables = new UpdatablesFastList<AbstractUpdatable>();

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
            Update(updatables, Time.deltaTime * timeScale);
            Update(smoothUpdatables, Time.smoothDeltaTime * timeScale);
        }

        private void LateUpdate()
        {
            if (playState != PlayState.Playing)
            {
                return;
            }
            Update(lateUpdatables, Time.deltaTime * timeScale);
            Update(smoothLateUpdatables, Time.smoothDeltaTime * timeScale);
        }

        private void FixedUpdate()
        {
            if (playState != PlayState.Playing)
            {
                return;
            }
            Update(fixedUpdatables, Time.fixedDeltaTime * timeScale);
        }

        public void CustomUpdate(float deltaTime)
        {
            if (playState != PlayState.Playing)
            {
                return;
            }
            Update(customUpdatables, deltaTime * timeScale);
        }

#pragma warning restore IDE0051, RCS1213

        internal static void Update(UpdatablesFastList<AbstractUpdatable> updatables, float scaledDeltaTime)
        {
            AbstractUpdatable index = updatables.anchor.next;

            while (index != null)
            {
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
                try
                {
#endif
                index.UpdateInternal(scaledDeltaTime);
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

        #region IUpdateController

        void IUpdateController.SubscribeToUpdate(AbstractUpdatable updatable)
        {
            switch (updatable.updateType)
            {
                case UpdateType.Update:
                    updatables.Add(updatable);
                    break;
                case UpdateType.SmoothUpdate:
                    smoothUpdatables.Add(updatable);
                    break;
                case UpdateType.LateUpdate:
                    lateUpdatables.Add(updatable);
                    break;
                case UpdateType.SmoothLateUpdate:
                    smoothLateUpdatables.Add(updatable);
                    break;
                case UpdateType.FixedUpdate:
                    fixedUpdatables.Add(updatable);
                    break;
                case UpdateType.Custom:
                    customUpdatables.Add(updatable);
                    break;
            }
        }

        void IUpdateController.UnsubscribeFromUpdate(AbstractUpdatable updatable)
        {
            switch (updatable.updateType)
            {
                case UpdateType.Update:
                    updatables.Remove(updatable);
                    break;
                case UpdateType.SmoothUpdate:
                    smoothUpdatables.Remove(updatable);
                    break;
                case UpdateType.LateUpdate:
                    lateUpdatables.Remove(updatable);
                    break;
                case UpdateType.SmoothLateUpdate:
                    smoothLateUpdatables.Remove(updatable);
                    break;
                case UpdateType.FixedUpdate:
                    fixedUpdatables.Remove(updatable);
                    break;
                case UpdateType.Custom:
                    customUpdatables.Remove(updatable);
                    break;
            }
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