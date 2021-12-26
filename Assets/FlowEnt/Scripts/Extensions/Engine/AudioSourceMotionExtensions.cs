using UnityEngine;
using FriedSynapse.FlowEnt.Motions.AudioSources;

namespace FriedSynapse.FlowEnt
{
    public static class AudioSourceMotionExtensions
    {
        #region Pitch

        /// <summary>
        /// Applies a <see cref="PitchMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotion<AudioSource> Pitch(this TweenMotion<AudioSource> tweenMotion, float value)
            => tweenMotion.Apply(new PitchMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="PitchToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<AudioSource> PitchTo(this TweenMotion<AudioSource> tweenMotion, float to)
            => tweenMotion.Apply(new PitchMotion(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="PitchToMotion" /> to the tween.
        /// </summary>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<AudioSource> PitchTo(this TweenMotion<AudioSource> tweenMotion, float from, float to)
            => tweenMotion.Apply(new PitchMotion(tweenMotion.Item, from, to));

        #endregion

        #region Volume

        /// <summary>
        /// Applies a <see cref="VolumeMotion" /> to the tween.
        /// </summary>
        /// <remarks>Calculated destination value (current value + <paramref name="value"/>) will be clamped between 0 and 1.</remarks>
        /// <param name="tweenMotion"></param>
        /// <param name="value"></param>
        public static TweenMotion<AudioSource> Volume(this TweenMotion<AudioSource> tweenMotion, float value)
            => tweenMotion.Apply(new VolumeMotion(tweenMotion.Item, value));

        /// <summary>
        /// Applies a <see cref="VolumeToMotion" /> to the tween.
        /// </summary>
        /// <remarks><paramref name="to"/> value will be clamped between 0 and 1.</remarks>
        /// <param name="tweenMotion"></param>
        /// <param name="to"></param>
        public static TweenMotion<AudioSource> VolumeTo(this TweenMotion<AudioSource> tweenMotion, float to)
            => tweenMotion.Apply(new VolumeMotion(tweenMotion.Item, null, to));

        /// <summary>
        /// Applies a <see cref="VolumeToMotion" /> to the tween.
        /// </summary>
        /// <remarks><paramref name="from"/> and <paramref name="to"/> values will be clamped between 0 and 1.</remarks>
        /// <param name="tweenMotion"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public static TweenMotion<AudioSource> VolumeTo(this TweenMotion<AudioSource> tweenMotion, float from, float to)
            => tweenMotion.Apply(new VolumeMotion(tweenMotion.Item, from, to));

        #endregion
    }
}