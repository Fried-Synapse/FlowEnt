using System;

namespace FriedSynapse.FlowEnt
{
    public partial class AbstractAnimation : IControllable
    {
        private const string ErrorNotManipulable = "Only Playing or Paused animations can be manipulated.";
        private const string ErrorNotSeekable = "Animation is not seekable. Check IsSeekable member.";

        float IControllable.ElapsedTime => elapsedTime;
        private protected abstract bool IsSeekable { get; }
        bool IControllable.IsSeekable => IsSeekable;
        private protected abstract float TotalSeekTime { get; }
        float IControllable.SeekRatio
        {
            get
            {
                if (!IsSeekable)
                {
                    throw new NotSeekableException(ErrorNotSeekable);
                }
                return elapsedTime / TotalSeekTime;
            }

            set
            {
                if (!IsSeekable)
                {
                    throw new NotSeekableException(ErrorNotSeekable);
                }

                HandleSimulationStates();

                UpdateInternal(GetSeekingDeltaTimeFromRatio(value));
            }
        }

        private protected abstract float GetSeekingDeltaTimeFromRatio(float ratio);

        /// <summary>
        /// Returns the current animation cast as IControllable, which can be used to control the animation
        /// </summary>
        public IControllable Controllable => this;

        void IControllable.Resume()
           => Resume();

        void IControllable.Pause()
            => Pause();

        void IControllable.SimulateFrames(int frameCount)
        {
            HandleSimulationStates();
            DeltaTimes deltaTimes = FlowEntController.Updater.GetDeltaTimes();
            float deltaTime = updateType switch
            {
                UpdateType.Update => deltaTimes.deltaTime,
                UpdateType.SmoothUpdate => deltaTimes.smoothDeltaTime,
                UpdateType.LateUpdate => deltaTimes.deltaTime,
                UpdateType.SmoothLateUpdate => deltaTimes.smoothDeltaTime,
                UpdateType.FixedUpdate => deltaTimes.fixedDeltaTime,
                UpdateType.Custom => deltaTimes.fixedDeltaTime,
                _ => throw new NotImplementedException(),
            };
            UpdateInternal(frameCount * deltaTime * FlowEntController.Instance.TimeScale);
        }

        void IControllable.SimulateUpdate(float deltaTime)
        {
            HandleSimulationStates();
            UpdateInternal(deltaTime * FlowEntController.Instance.TimeScale);
        }

        void IControllable.Stop()
            => Stop();

        private void HandleSimulationStates()
        {
            switch (playState)
            {
                case PlayState.Building:
                case PlayState.Waiting:
                    throw new InvalidOperationException(ErrorNotManipulable);
                case PlayState.Finished:
                    overdraft = null;
                    playState = PlayState.Paused;
                    break;
            }
        }
    }
}
