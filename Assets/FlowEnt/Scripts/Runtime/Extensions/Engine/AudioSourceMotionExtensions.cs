using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Tween.AudioSources;

namespace FriedSynapse.FlowEnt
{
    public static class AudioSourceMotionExtensions
    {
        #region Pitch

        /// <summary>
        /// Applies a <see cref="PitchMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<AudioSource> Pitch(this TweenMotionProxy<AudioSource> proxy, float value)
            => proxy.Apply(new PitchMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="PitchMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<AudioSource> PitchTo(this TweenMotionProxy<AudioSource> proxy, float to)
            => proxy.Apply(new PitchMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="PitchMotion" /> to the tween.
        /// </summary>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<AudioSource> PitchTo(this TweenMotionProxy<AudioSource> proxy, float from, float to)
            => proxy.Apply(new PitchMotion(proxy.Item, from, to));

        #endregion

        #region Volume

        /// <summary>
        /// Applies a <see cref="VolumeMotion" /> to the tween.
        /// </summary>
        /// <remarks>Calculated destination value (current value + <paramref name="value"/>) will be clamped between 0 and 1.</remarks>
        /// <param name="proxy"></param>
        /// <param name="value"></param>
        public static TweenMotionProxy<AudioSource> Volume(this TweenMotionProxy<AudioSource> proxy, float value)
            => proxy.Apply(new VolumeMotion(proxy.Item, value));

        /// <summary>
        /// Applies a <see cref="VolumeMotion" /> to the tween.
        /// </summary>
        /// <remarks><paramref name="to"/> value will be clamped between 0 and 1.</remarks>
        /// <param name="proxy"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<AudioSource> VolumeTo(this TweenMotionProxy<AudioSource> proxy, float to)
            => proxy.Apply(new VolumeMotion(proxy.Item, default, to));

        /// <summary>
        /// Applies a <see cref="VolumeMotion" /> to the tween.
        /// </summary>
        /// <remarks><paramref name="from"/> and <paramref name="to"/> values will be clamped between 0 and 1.</remarks>
        /// <param name="proxy"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotionProxy<AudioSource> VolumeTo(this TweenMotionProxy<AudioSource> proxy, float from, float to)
            => proxy.Apply(new VolumeMotion(proxy.Item, from, to));

        #endregion
    }
}