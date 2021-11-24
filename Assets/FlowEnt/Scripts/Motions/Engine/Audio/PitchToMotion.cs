using UnityEngine;

namespace FriedSynapse.FlowEnt.Motions.Audio
{
    /// <summary>
    /// Lerps the <see cref="AudioSource.pitch" /> value.
    /// </summary>
    public class PitchToMotion : AbstractMotion<AudioSource>
    {
        public PitchToMotion(AudioSource item, float to) : base(item)
        {
            this.to = to;
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
                from = item.pitch;
            }
        }

        public override void OnUpdate(float t)
        {
            item.pitch = Mathf.LerpUnclamped(from, to, t);
        }
    }
}