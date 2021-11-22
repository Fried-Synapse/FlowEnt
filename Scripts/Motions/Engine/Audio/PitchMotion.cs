using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Audio
{
    public class PitchMotion : AbstractMotion<AudioSource>
    {
        private const float MinPitch = -3;
        private const float MaxPitch = 3;

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
            to = Mathf.Clamp(from + value, MinPitch, MaxPitch);
        }

        public override void OnUpdate(float t)
        {
            item.pitch = Mathf.Lerp(from, to, t);
        }
    }
}