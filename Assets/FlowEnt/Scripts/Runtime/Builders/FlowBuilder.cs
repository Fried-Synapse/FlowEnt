using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class FlowBuilder : AbstractAnimationBuilder<Flow>
    {
        [Serializable]
        public class Queue
        {
#pragma warning disable IDE0044, RCS1169
            [SerializeField]
            private float startTime;
            public float StartTime => startTime;
            [SerializeReference]
            private List<IAbstractAnimationBuilder> animations;
            public List<IAbstractAnimationBuilder> Animations => animations;
#pragma warning restore IDE0044, RCS1169
        }
#pragma warning disable IDE0044, RCS1169
        [SerializeField]
        private FLowOptionsBuilder options;
        public FLowOptionsBuilder Options => options;
        [SerializeReference]
        private List<Queue> queues;
        public List<Queue> Queues => queues;
#pragma warning restore IDE0044, RCS1169

        public override Flow Build()
        {
            Flow flow = new Flow(Options.Build());
            foreach (Queue queue in Queues)
            {
                int animationCount = queue.Animations.Count;
                if (animationCount == 0)
                {
                    continue;
                }

                flow.At(queue.StartTime, queue.Animations[0].Build());
                for (int i = 1; i < queue.Animations.Count; i++)
                {
                    flow.Queue(queue.Animations[i].Build());
                }
            }
            return flow;
        }
    }
}
