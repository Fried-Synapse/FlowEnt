using UnityEditor;
using UnityEngine;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoEventsBuilder))]
    public class EchoEventsBuilderPropertyDrawer : AbstractEventsBuilderPropertyDrawer<EchoEventsBuilderPropertyDrawer.PropertiesEnum>
    {
        public enum PropertiesEnum
        {
            onStarting,
            onStarted,
            onUpdating,
            onUpdated,
            onLoopCompleted,
            onCompleting,
            onCompleted,
        }
    }
}
