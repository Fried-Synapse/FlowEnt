using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Audio;

namespace FriedSynapse.FlowEnt
{
    public static class AudioMotionExtensions
    {
        #region Pitch

        /// <summary>
        /// Lerps the pitch of an <see cref="AudioSource" /> from its current pitch to its current pitch plus the given <paramref name="value" />.
        /// <para>NOTE: Final value is clamped between the minimum and maximum possible pitch values. i.e -3 to 3 respectively</para>
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TweenMotion<AudioSource> Pitch(this TweenMotion<AudioSource> motion, float value)
            => motion.Apply(new PitchMotion(motion.Item, value));

        /// <summary>
        /// Lerps the pitch of an <see cref="AudioSource" /> from its current pitch to the specified <paramref name="to" /> value.
        /// <para>NOTE: <paramref name="to" /> value will be clamped between the minimum and maximum possible pitch values. i.e -3 to 3 respectively</para>
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static TweenMotion<AudioSource> PitchTo(this TweenMotion<AudioSource> motion, float to)
            => motion.Apply(new PitchToMotion(motion.Item, to));

        /// <summary>
        /// Lerps the pitch of an <see cref="AudioSource" /> from specified <paramref name="from" /> value to the specified <paramref name="to" /> value.
        /// <para>NOTE: Both <paramref name="from" /> and <paramref name="to" /> value will be clamped between the minimum and maximum possible pitch values. i.e -3 to 3 respectively</para>
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static TweenMotion<AudioSource> PitchTo(this TweenMotion<AudioSource> motion, float from, float to)
            => motion.Apply(new PitchToMotion(motion.Item, from, to));

        #endregion

        #region Volume

        /// <summary>
        /// Lerps the volume of an <see cref="AudioSource" /> from its current volume to its current volume plus the given <paramref name="value" />.
        /// <para>NOTE: Final value is clamped between the minimum and maximum possible volume values. i.e 0 to 1 respectively</para>
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TweenMotion<AudioSource> Volume(this TweenMotion<AudioSource> motion, float value)
                => motion.Apply(new VolumeMotion(motion.Item, value));

        /// <summary>
        /// Lerps the volume of an <see cref="AudioSource" /> from its current volume to the specified <paramref name="to" /> value.
        /// <para>NOTE: <paramref name="to" /> value will be clamped between the minimum and maximum possible volume values. i.e 0 to 1 respectively</para>
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static TweenMotion<AudioSource> VolumeTo(this TweenMotion<AudioSource> motion, float to)
            => motion.Apply(new VolumeToMotion(motion.Item, to));

        /// <summary>
        /// Lerps the volume of an <see cref="AudioSource" /> from specified <paramref name="from" /> value to the specified <paramref name="to" /> value.
        /// <para>NOTE: Both <paramref name="from" /> and <paramref name="to" /> value will be clamped between the minimum and maximum possible volume values. i.e 0 to 1 respectively</para>
        /// </summary>
        /// <param name="motion"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static TweenMotion<AudioSource> VolumeTo(this TweenMotion<AudioSource> motion, float from, float to)
            => motion.Apply(new VolumeToMotion(motion.Item, from, to));

        #endregion
    }
}