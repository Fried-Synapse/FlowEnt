using System;

namespace FriedSynapse.FlowEnt
{
    /// <summary>
    /// Used to control something that is playable.
    /// Provides seek functionality.
    /// </summary>
    public interface IControllable
    {
        float TimeScale { get; set; }
        PlayState PlayState { get; }
        /// <summary>
        /// Amount of scaled time that passed within this animation.
        /// </summary>
        float ElapsedTime { get; }
        /// <summary>
        /// Returns true if the current Controllable can be seeked. Use SeekRation in that case.
        /// </summary>
        bool IsSeekable { get; }
        /// <summary>
        /// Get or Set the seek ration. Make sure the IsSeekable is true, otherwise this will throw an exception.
        /// </summary>
        float SeekRatio { get; set; }
        /// <summary>
        /// Simulates <paramref name="frameCount"/> number of frames to pass. If negative, it'll go backwards in time.
        /// </summary>
        /// <param name="frameCount"></param>
        void SimulateFrames(int frameCount);
        /// <summary>
        /// Sends an update with the specified delta time to the Controllable.
        /// </summary>
        /// <param name="deltaTime"></param>
        void SimulateUpdate(float deltaTime);
        void Resume();
        void Pause();
        void Stop();
    }

    public class NotSeekableException : InvalidOperationException
    {
        public NotSeekableException()
        {
        }

        public NotSeekableException(string message) : base(message)
        {
        }

        public NotSeekableException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
