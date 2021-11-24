using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Audio;

namespace FriedSynapse.FlowEnt
{
    public static class AudioMotionExtensions
    {
        #region Pitch

        /// <summary>
        /// Applies a <see cref="PitchMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        public static TweenMotion<AudioSource> Pitch(this TweenMotion<AudioSource> motion, float value)
            => motion.Apply(new PitchMotion(motion.Item, value));

        /// <summary>
        /// Applies a <see cref="PitchToMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        public static TweenMotion<AudioSource> PitchTo(this TweenMotion<AudioSource> motion, float to)
            => motion.Apply(new PitchToMotion(motion.Item, to));

        /// <summary>
        /// Applies a <see cref="PitchToMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        public static TweenMotion<AudioSource> PitchTo(this TweenMotion<AudioSource> motion, float from, float to)
            => motion.Apply(new PitchToMotion(motion.Item, from, to));

        #endregion

        #region Volume

        /// <summary>
        /// Applies a <see cref="VolumeMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        public static TweenMotion<AudioSource> Volume(this TweenMotion<AudioSource> motion, float value)
                => motion.Apply(new VolumeMotion(motion.Item, value));

        /// <summary>
        /// Applies a <see cref="VolumeToMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        public static TweenMotion<AudioSource> VolumeTo(this TweenMotion<AudioSource> motion, float to)
            => motion.Apply(new VolumeToMotion(motion.Item, to));

        /// <summary>
        /// Applies a <see cref="VolumeToMotion" /> to the tween.
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        public static TweenMotion<AudioSource> VolumeTo(this TweenMotion<AudioSource> motion, float from, float to)
            => motion.Apply(new VolumeToMotion(motion.Item, from, to));

        #endregion
    }
}