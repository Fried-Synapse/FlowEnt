using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.AudioSources
{
    /// <summary>
    /// Lerps the <see cref="AudioSource.pitch" /> value.
    /// </summary>
    public class PitchMotion : AbstractFloatMotion<AudioSource>
    {
        public PitchMotion(AudioSource item, float value) : base(item, value)
        {
        }

        public PitchMotion(AudioSource item, float? from, float to) : base(item, from, to)
        {
        }

        protected override float GetFrom() => item.pitch;
        protected override void SetValue(float value) => item.pitch = value;
    }
}