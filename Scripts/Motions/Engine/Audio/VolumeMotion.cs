using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Audio
{
    public class VolumeMotion : AbstractMotion<AudioSource>
    {
        public VolumeMotion(AudioSource item, float value) : base(item)
        {
            this.value = value;
        }

        private readonly float value;
        private float from;
        private float to;

        public override void OnStart()
        {
            from = item.volume;
            to = Mathf.Clamp01(from + value);
        }

        public override void OnUpdate(float t)
        {
            item.volume = Mathf.Lerp(from, to, t);
        }
    }
}