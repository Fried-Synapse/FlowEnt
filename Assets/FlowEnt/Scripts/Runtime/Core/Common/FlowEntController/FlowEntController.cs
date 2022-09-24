using System;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    internal struct DeltaTimes
    {
        public float deltaTime;
        public float smoothDeltaTime;
        public float lateDeltaTime;
        public float lateSmoothDeltaTime;
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
        #region Singleton

        private static readonly object lockObject = new object();
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
                            GameObject gameObject = new GameObject("FlowEnt");
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

        internal readonly UpdatablesFastList<AbstractUpdatable> updatables = new UpdatablesFastList<AbstractUpdatable>();
        internal readonly UpdatablesFastList<AbstractUpdatable> smoothUpdatables = new UpdatablesFastList<AbstractUpdatable>();
        internal readonly UpdatablesFastList<AbstractUpdatable> lateUpdatables = new UpdatablesFastList<AbstractUpdatable>();
        internal readonly UpdatablesFastList<AbstractUpdatable> smoothLateUpdatables = new UpdatablesFastList<AbstractUpdatable>();
        internal readonly UpdatablesFastList<AbstractUpdatable> fixedUpdatables = new UpdatablesFastList<AbstractUpdatable>();
        internal readonly UpdatablesFastList<AbstractUpdatable> customUpdatables = new UpdatablesFastList<AbstractUpdatable>();

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

        internal void Update(float deltaTime, float smoothDeltaTime)
        {
            if (playState != PlayState.Playing)
            {
                return;
            }
            Update(updatables, deltaTime * timeScale);
            Update(smoothUpdatables, smoothDeltaTime * timeScale);
        }

        internal void LateUpdate(float deltaTime, float smoothDeltaTime)
        {
            if (playState != PlayState.Playing)
            {
                return;
            }
            Update(lateUpdatables, deltaTime * timeScale);
            Update(smoothLateUpdatables, smoothDeltaTime * timeScale);
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

        internal static void Update(UpdatablesFastList<AbstractUpdatable> updatables, float scaledDeltaTime)
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
                        $"<color={FlowEntConstants.Red}><b>Exception on update</b></color>\n" +
                        $"<color={FlowEntConstants.Orange}><b>Origin of animation that generated the exception</b></color>:\n" +
                        $"<color={FlowEntConstants.Orange}>{index.stackTrace}</color>\n\n" +
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
            Stop(updatables, triggerOnCompleted);
            Stop(smoothUpdatables, triggerOnCompleted);
            Stop(lateUpdatables, triggerOnCompleted);
            Stop(smoothLateUpdatables, triggerOnCompleted);
            Stop(fixedUpdatables, triggerOnCompleted);
            Stop(customUpdatables, triggerOnCompleted);
        }

        private static void Stop(UpdatablesFastList<AbstractUpdatable> updatables, bool triggerOnCompleted)
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

        void IControllable.ChangeFrame(int frameCount)
        {
            if (playState == PlayState.Playing)
            {
                Pause();
            }
            float scaledFrameCount = timeScale * frameCount;
            DeltaTimes deltaTimes = updater.GetDeltaTimes();
            Update(updatables, deltaTimes.deltaTime * scaledFrameCount);
            Update(smoothUpdatables, deltaTimes.smoothDeltaTime * scaledFrameCount);
            Update(lateUpdatables, deltaTimes.lateDeltaTime * scaledFrameCount);
            Update(smoothLateUpdatables, deltaTimes.lateSmoothDeltaTime * scaledFrameCount);
            Update(fixedUpdatables, deltaTimes.fixedDeltaTime * scaledFrameCount);
            Update(customUpdatables, deltaTimes.fixedDeltaTime * scaledFrameCount);
        }

        void IControllable.ManualUpdate(float deltaTime)
        {
            float scaledDeltaTime = timeScale * deltaTime;
            Update(updatables, scaledDeltaTime);
            Update(smoothUpdatables, scaledDeltaTime);
            Update(lateUpdatables, scaledDeltaTime);
            Update(smoothLateUpdatables, scaledDeltaTime);
            Update(fixedUpdatables, scaledDeltaTime);
            Update(customUpdatables, scaledDeltaTime);
        }

        void IControllable.Stop()
            => Stop();

        #endregion
    }
}