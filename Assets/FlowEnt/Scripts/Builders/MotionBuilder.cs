
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    public interface IMotionBuilder : IBuilder<IMotion>
    {
    }

    [Serializable]
    public class MotionsBuilder : IBuilder<List<IMotion>>
    {
#pragma warning disable IDE0044, RCS1169
        [SerializeField]
        private List<IMotionBuilder> motionBuilders;
        public List<IMotionBuilder> MotionBuilders => motionBuilders;
#pragma warning restore IDE0044, RCS1169

        public List<IMotion> Build()
        {
            List<IMotion> motions = new List<IMotion>(motionBuilders.Count);
            foreach (IMotionBuilder motionBuilder in motionBuilders)
            {
                motions.Add(motionBuilder.Build());
            }
            return motions;
        }
    }
}
