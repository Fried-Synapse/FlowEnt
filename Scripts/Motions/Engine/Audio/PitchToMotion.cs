using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Audio
{
    public class PitchToMotion : AbstractMotion<AudioSource>
    {
        private const float MinPitch = -3;
        private const float MaxPitch = 3;

        public PitchToMotion(AudioSource item, float to) : base(item)
        {
            this.to = Mathf.Clamp(to, MinPitch, MaxPitch);
        }

        public PitchToMotion(AudioSource item, float from, float to) : this(item, to)
        {
            hasFrom = true;
            this.from = from;
        }

        private readonly bool hasFrom;
        private float from;
        private readonly float to;

        public override void OnStart()
        {
            if (!hasFrom)
            {
                from = item.volume;
            }
        }

        public override void OnUpdate(float t)
        {
            item.pitch = Mathf.Lerp(from, to, t);
        }
    }
}