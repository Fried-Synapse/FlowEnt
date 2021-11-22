using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Audio
{
    public class VolumeToMotion : AbstractMotion<AudioSource>
    {
        public VolumeToMotion(AudioSource item, float to) : base(item)
        {
            this.to = Mathf.Clamp01(to);
        }

        public VolumeToMotion(AudioSource item, float from, float to) : this(item, to)
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
            item.volume = Mathf.Lerp(from, to, t);
        }
    }
}