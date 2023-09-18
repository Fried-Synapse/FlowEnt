using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class FlowBuilder : AbstractAnimationBuilder<Flow>
    {
        [Serializable]
        public class QueueList : AbstractListBuilder<QueueList.Queue, List<AbstractAnimation>>
        {
            [Serializable]
            public class Queue : AbstractListBuilder<IAbstractAnimationBuilder, AbstractAnimation>,
                IIdentifiableBuilder, IListBuilderItem
            {
                [SerializeField]
                private string displayName;

                public string DisplayName => displayName;

                [SerializeField]
                private bool isDisplayNameEnabled;

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
        private FlowOptionsBuilder options;

        public FlowOptionsBuilder Options => options;

        [SerializeField]
        private FlowEventsBuilder events;

        public FlowEventsBuilder Events => events;

        [SerializeField]
        private QueueList queues;

        public QueueList Queues => queues;

        public override Flow Build()
        {
            Flow flow = new Flow(Options.Build())
                .SetEvents(Events.Build());
            foreach (QueueList.Queue queue in Queues.Items)
            {
                int animationCount = queue.Items.Count;
                if (!queue.IsEnabled || animationCount == 0)
                {
                    continue;
                }

                flow.At(queue.StartTime, queue.Items[0].Build());
                for (int i = 1; i < queue.Items.Count; i++)
                {
                    flow.Queue(queue.Items[i].Build());
                }
            }

            return flow;
        }
    }
}