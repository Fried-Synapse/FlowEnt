using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Audio
{
    /// <summary>
    /// Lerps a <see cref="float" /> value representing <see cref="AudioSource.pitch" />.
    /// </summary>
    public class PitchMotion : AbstractMotion<AudioSource>
    {
        public PitchMotion(AudioSource item, float value) : base(item)
        {
            this.value = value;
        }

        private readonly float value;
        private float from;
        private float to;

        public override void OnStart()
        {
            from = item.pitch;
            to = from + value;
        }

        public override void OnUpdate(float t)
        {
            item.pitch = Mathf.LerpUnclamped(from, to, t);
        }
    }
}