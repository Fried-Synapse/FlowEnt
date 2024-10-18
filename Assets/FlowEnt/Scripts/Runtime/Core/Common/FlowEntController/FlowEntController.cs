using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    internal struct DeltaTimes
    {
        public float deltaTime;
        public float smoothDeltaTime;
        public float unscaledDeltaTime;
        public float fixedDeltaTime;
    }

    internal interface IFlowEntUpdater
    {
        internal void SetController(FlowEntController controller);
        internal DeltaTimes GetDeltaTimes();
    }

    /// <summary>
    /// A singleton that is the core of the FlowEnt library. Hold the main update method and a handful of global parameters.
    /// The script automatically attaches itself to an object in the scene called FlowEnt(created by this script).
    /// Make sure not to delete this object because the library will not work without this script attached to an object in the scene.
    /// </summary>
    public class FlowEntController :
        IUpdateController,
        IControllable
    {
        private const string ErrorNotManipulable = "Only Playing or Paused engine can be manipulated.";

        #region Singleton

        private static readonly object lockObject = new();
        private static IFlowEntUpdater updater;
        internal static IFlowEntUpdater Updater => updater;
        private static FlowEntController instance;

        public static FlowEntController Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new FlowEntController();

                        static void initRuntime()
                        {
                            GameObject gameObject = new("FlowEnt");
                            gameObject.hideFlags = HideFlags.HideInHierarchy;
                            updater = gameObject.AddComponent<FlowEntRuntimeUpdater>();
                        }
#if UNITY_EDITOR
                        if (Application.isPlaying)
                        {
                            initRuntime();
                        }
                        else
                        {
                            updater = new FlowEntEditorUpdater();
                        }
#else
                        initRuntime();
#endif
                        updater.SetController(instance);
                    }
                }

                return instance;
            }
        }

        public static bool HasInstance => instance != null;

        internal void ResetInstance()
        {
            instance = null;
        }

        #endregion

        #region Members

        private readonly UpdatablesFastList updatables = new();
        private readonly UpdatablesFastList smoothUpdatables = new();
        private readonly UpdatablesFastList unscaledUpdatables = new();
        private readonly UpdatablesFastList lateUpdatables = new();
        private readonly UpdatablesFastList smoothLateUpdatables = new();
        private readonly UpdatablesFastList unscaledLateUpdatables = new();
        private readonly UpdatablesFastList fixedUpdatables = new();
        private readonly UpdatablesFastList customUpdatables = new();

        private static float elapsedTime;
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
                    throw new ArgumentException("Value cannot be less than 0.");
                }

                timeScale = value;
            }
        }

        private PlayState playState = PlayState.Playing;

        /// <summary>
        /// The current state of animations.
        /// </summary>
        public PlayState PlayState { get => playState; }

        #endregion

        #region Update

        internal void Update(float deltaTime, float smoothDeltaTime, float unscaledDeltaTime)
        {
            if (playState != PlayState.Playing)
            {
                return;
            }

            Update(updatables, deltaTime * timeScale);
            Update(smoothUpdatables, smoothDeltaTime * timeScale);
            Update(unscaledUpdatables, unscaledDeltaTime * timeScale);
        }

        internal void LateUpdate(float deltaTime, float smoothDeltaTime, float unscaledDeltaTime)
        {
            if (playState != PlayState.Playing)
            {
                return;
            }

            Update(lateUpdatables, deltaTime * timeScale);
            Update(smoothLateUpdatables, smoothDeltaTime * timeScale);
            Update(unscaledLateUpdatables, unscaledDeltaTime * timeScale);
        }

        internal void FixedUpdate(float deltaTime)
        {
            if (playState != PlayState.Playing)
            {
                return;
            }

            Update(fixedUpdatables, deltaTime * timeScale);
        }

        public void CustomUpdate(float deltaTime)
        {
            if (playState != PlayState.Playing)
            {
                return;
            }

            Update(customUpdatables, deltaTime * timeScale);
        }

        private static void Update(UpdatablesFastList updatables, float scaledDeltaTime)
        {
            elapsedTime += scaledDeltaTime;
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
                        $"<color={FlowEntInternalConstants.Red}><b>Exception on update</b></color>\n" +
                        $"<color={FlowEntInternalConstants.Orange}><b>Origin of animation that generated the exception</b></color>:\n" +
                        $"<color={FlowEntInternalConstants.Orange}>{index.stackTrace}</color>\n\n" +
                        $"<b>Exception</b>:\n{ex}");
                }
#endif
                index = index.next;
            }
        }

        #endregion

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
                case UpdateType.UnscaledUpdate:
                    unscaledUpdatables.Add(updatable);
                    break;
                case UpdateType.LateUpdate:
                    lateUpdatables.Add(updatable);
                    break;
                case UpdateType.SmoothLateUpdate:
                    smoothLateUpdatables.Add(updatable);
                    break;
                case UpdateType.UnscaledLateUpdate:
                    unscaledLateUpdatables.Add(updatable);
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
                case UpdateType.UnscaledUpdate:
                    unscaledUpdatables.Remove(updatable);
                    break;
                case UpdateType.LateUpdate:
                    lateUpdatables.Remove(updatable);
                    break;
                case UpdateType.SmoothLateUpdate:
                    smoothLateUpdatables.Remove(updatable);
                    break;
                case UpdateType.UnscaledLateUpdate:
                    unscaledLateUpdatables.Remove(updatable);
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
            Stop(updatables, triggerOnCompleted);
            Stop(smoothUpdatables, triggerOnCompleted);
            Stop(unscaledUpdatables, triggerOnCompleted);
            Stop(lateUpdatables, triggerOnCompleted);
            Stop(smoothLateUpdatables, triggerOnCompleted);
            Stop(unscaledLateUpdatables, triggerOnCompleted);
            Stop(fixedUpdatables, triggerOnCompleted);
            Stop(customUpdatables, triggerOnCompleted);
        }

        private static void Stop(UpdatablesFastList updatables, bool triggerOnCompleted)
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

        #region IControllable

        float IControllable.ElapsedTime => elapsedTime;
        bool IControllable.IsSeekable => false;
        float IControllable.SeekRatio { get => throw new NotSeekableException(); set => throw new NotSeekableException(); }

        void IControllable.SimulateFrames(int frameCount)
        {
            HandleSimulationStates();
            float scaledFrameCount = timeScale * frameCount;
            DeltaTimes deltaTimes = updater.GetDeltaTimes();
            Update(updatables, deltaTimes.deltaTime * scaledFrameCount);
            Update(smoothUpdatables, deltaTimes.smoothDeltaTime * scaledFrameCount);
            Update(unscaledUpdatables, deltaTimes.unscaledDeltaTime * scaledFrameCount);
            Update(lateUpdatables, deltaTimes.deltaTime * scaledFrameCount);
            Update(smoothLateUpdatables, deltaTimes.smoothDeltaTime * scaledFrameCount);
            Update(unscaledLateUpdatables, deltaTimes.unscaledDeltaTime * scaledFrameCount);
            Update(fixedUpdatables, deltaTimes.fixedDeltaTime * scaledFrameCount);
            Update(customUpdatables, deltaTimes.fixedDeltaTime * scaledFrameCount);
        }

        void IControllable.SimulateUpdate(float deltaTime)
        {
            HandleSimulationStates();
            float scaledDeltaTime = timeScale * deltaTime;
            Update(updatables, scaledDeltaTime);
            Update(smoothUpdatables, scaledDeltaTime);
            Update(unscaledUpdatables, scaledDeltaTime);
            Update(lateUpdatables, scaledDeltaTime);
            Update(smoothLateUpdatables, scaledDeltaTime);
            Update(unscaledLateUpdatables, scaledDeltaTime);
            Update(fixedUpdatables, scaledDeltaTime);
            Update(customUpdatables, scaledDeltaTime);
        }

        void IControllable.Stop()
            => Stop();

        private void HandleSimulationStates()
        {
            switch (playState)
            {
                case PlayState.Building:
                case PlayState.Waiting:
                case PlayState.Finished:
                    throw new InvalidOperationException(ErrorNotManipulable);
            }
        }

        #endregion
    }
}