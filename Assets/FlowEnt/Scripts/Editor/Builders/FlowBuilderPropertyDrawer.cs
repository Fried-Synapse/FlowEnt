using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(FlowBuilder))]
    public class FlowBuilderPropertyDrawer : AbstractAnimationBuilderPropertyDrawer<Flow, FlowBuilder>
    {
        protected override List<string> VisibleProperties => new List<string>{
            "options",
            "events",
            "queues",
        };
    }
}
