using UnityEngine;
using FriedSynapse.FlowEnt.Motions.Audio;

namespace FriedSynapse.FlowEnt
{
    public static class AudioMotionExtensions
    {
        #region Pitch

        public static TweenMotion<AudioSource> Pitch(this TweenMotion<AudioSource> motion, float value)
            => motion.Apply(new PitchMotion(motion.Item, value));

        public static TweenMotion<AudioSource> PitchTo(this TweenMotion<AudioSource> motion, float to)
            => motion.Apply(new PitchToMotion(motion.Item, to));

        public static TweenMotion<AudioSource> PitchTo(this TweenMotion<AudioSource> motion, float from, float to)
            => motion.Apply(new PitchToMotion(motion.Item, from, to));

        #endregion

        #region Volume

        public static TweenMotion<AudioSource> Volume(this TweenMotion<AudioSource> motion, float value)
                => motion.Apply(new VolumeMotion(motion.Item, value));

        public static TweenMotion<AudioSource> VolumeTo(this TweenMotion<AudioSource> motion, float to)
            => motion.Apply(new VolumeToMotion(motion.Item, to));

        public static TweenMotion<AudioSource> VolumeTo(this TweenMotion<AudioSource> motion, float from, float to)
            => motion.Apply(new VolumeToMotion(motion.Item, from, to));

        #endregion
    }
}