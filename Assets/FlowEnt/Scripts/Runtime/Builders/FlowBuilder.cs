using System;
using System.Collections.Generic;
using UnityEngine;

namespace FriedSynapse.FlowEnt
{
    [Serializable]
    public class FlowBuilder : AbstractAnimationBuilder<Flow>
    {
        [Serializable]
        public class QueueList : AbstractListBuilder<QueueList.Queue>
        {
            [Serializable]
            public class Queue : AbstractListBuilder<IAbstractAnimationBuilder>, IBuilderListItem
            {
#pragma warning disable IDE0044, RCS1169, RCS1213, IDE0051, CS0414
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
#pragma warning restore IDE0044, RCS1169, RCS1213, IDE0051, CS0414
            }
        }

#pragma warning disable IDE0044, RCS1169
        [SerializeField]
        private FlowOptionsBuilder options;
        public FlowOptionsBuilder Options => options;
        [SerializeField]
        private FlowEventsBuilder events;
        public FlowEventsBuilder Events => events;
        [SerializeField]
        private QueueList queues;
        public QueueList Queues => queues;
#pragma warning restore IDE0044, RCS1169

        public override Flow Build()
            => SetAll(new Flow());

        public Flow Build(IUpdateController updateController)
            => SetAll(new Flow(updateController));

        private Flow SetAll(Flow flow)
        {
            flow.SetOptions(Options.Build());
            flow.SetEvents(Events.Build());
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
