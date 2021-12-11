using System;
using System.Collections.Generic;
using FriedSynapse.FlowEnt.Motions.Transforms;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class MotionsBuilder : IBuilder<List<IMotion>>
    {
#pragma warning disable IDE0044, RCS1169
        [SerializeField]
        private List<SerializableMotionBuilder> motionBuilders = new List<SerializableMotionBuilder>() { new MoveVectorMotionBuilder() };
        public List<SerializableMotionBuilder> MotionBuilders => motionBuilders;
#pragma warning restore IDE0044, RCS1169

        public List<IMotion> Build()
        {
            List<IMotion> motions = new List<IMotion>(motionBuilders.Count);
            // foreach (AbstractMotionBuilder motionBuilder in motionBuilders)
            // {
            //     motions.Add(motionBuilder.Build());
            // }
            return motions;
        }
    }
}
