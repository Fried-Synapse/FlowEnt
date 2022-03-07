using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(FlowEventsBuilder))]
    public class FlowEventsBuilderPropertyDrawer : AbstractEventsBuilderPropertyDrawer<FlowEventsBuilderPropertyDrawer.PropertiesEnum>
    {
        public enum PropertiesEnum
        {
            onStarted,
            onUpdated,
            onLoopCompleted,
            onCompleted,
        }
    }
}
