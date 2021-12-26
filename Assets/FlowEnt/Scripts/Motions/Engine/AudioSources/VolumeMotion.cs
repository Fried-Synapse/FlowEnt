using FriedSynapse.FlowEnt.Motions.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.AudioSources
{
    /// <summary>
    /// Lerps the <see cref="AudioSource.volume" /> value.
    /// </summary>
    public class VolumeMotion : AbstractFloatMotion<AudioSource>
    {
        public VolumeMotion(AudioSource item, float value) : base(item, value)
        {
        }

        public VolumeMotion(AudioSource item, float? from, float to) : base(item, from == null ? default : Mathf.Clamp01(from.Value), Mathf.Clamp01(to))
        {
        }

        protected override float GetFrom() => item.volume;
        protected override float GetTo(float from, float value) => Mathf.Clamp01(from + value);
        protected override void SetValue(float value) => item.volume = value;
    }
}