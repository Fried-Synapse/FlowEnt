using System;
using FriedSynapse.FlowEnt.Motions.Tween.Abstract;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Tween.AudioSources
{
    /// <summary>
    /// Lerps the <see cref="AudioSource.volume" /> value.
    /// </summary>
    public class VolumeMotion : AbstractFloatMotion<AudioSource>
    {
        [Serializable]
        public class ValueBuilder : AbstractValueBuilder
        {
            public override AbstractTweenMotion Build()
                => new VolumeMotion(item, value);
        }

        [Serializable]
        public class FromToBuilder : AbstractFromToBuilder
        {
            public override AbstractTweenMotion Build()
                => new VolumeMotion(item, From, to);
        }

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