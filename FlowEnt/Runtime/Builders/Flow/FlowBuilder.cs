using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class FlowBuilder : AbstractAnimationBuilder<Flow>, IGizmoDrawer
    {
        [Serializable]
        public class QueueList : AbstractListBuilder<QueueList.Queue, List<AbstractAnimation>>
        {
            [Serializable]
            public class Queue : AbstractListBuilder<IAbstractAnimationBuilder, AbstractAnimation>, IListBuilderItem
            {
                [SerializeField]
                private DisplayName displayName;

                public DisplayName DisplayName => displayName;

                [SerializeField]
                private bool isEnabled = true;

                public bool IsEnabled => isEnabled;

                [SerializeField]
                private float startTime;

                public float StartTime => startTime;

                public override List<AbstractAnimation> Build()
                    => Items.ConvertAll(m => m.Build());
            }

            public override List<List<AbstractAnimation>> Build()
                => Items.FindAll(m => m.IsEnabled).ConvertAll(m => m.Build());
        }

        [SerializeField]
        private FlowOptionsBuilder options = new();

        public FlowOptionsBuilder Options => options;

        [SerializeField]
        private FlowEventsBuilder events = new();

        public FlowEventsBuilder Events => events;

        [SerializeField]
        private QueueList queues = new();

        public QueueList Queues => queues;

        public override Flow Build()
        {
            Flow flow = new Flow(Options.Build())
#if FlowEnt_Debug || (UNITY_EDITOR && FlowEnt_Debug_Editor)
                .SetHierarchy<Flow>(hierarchy)
#endif
                .SetEvents(Events.Build());
            foreach (QueueList.Queue queue in Queues.Items)
            {
                int animationCount = queue.Items.Count;
                if (!queue.IsEnabled || animationCount == 0)
                {
                    continue;
                }

                flow.At(queue.StartTime, queue.Items.Where(m => m.IsEnabled).Select(m => m.Build()).ToList());
            }

            return flow;
        }
        
#if UNITY_EDITOR
        void IGizmoDrawer.OnGizmosDrawing(GizmoOptions options)
        {
            foreach (IAbstractAnimationBuilder animation in Queues.Items.SelectMany(queue => queue.Items))
            {
                if (animation.IsEnabled && animation is IGizmoDrawer drawer)
                {
                    drawer.OnGizmosDrawing(options);
                }
            }
        }
#endif
    }
}