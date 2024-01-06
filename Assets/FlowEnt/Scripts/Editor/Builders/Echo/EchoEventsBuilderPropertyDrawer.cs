using UnityEditor;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(EchoEventsBuilder))]
    public class EchoEventsBuilderPropertyDrawer : AbstractEventsBuilderPropertyDrawer<EchoEventsBuilderPropertyDrawer.FieldsEnum>
    {
        public enum FieldsEnum
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
