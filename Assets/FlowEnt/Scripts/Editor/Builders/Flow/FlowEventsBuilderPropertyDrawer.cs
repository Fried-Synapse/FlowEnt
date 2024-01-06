using UnityEditor;

namespace FriedSynapse.FlowEnt.Editor
{
    [CustomPropertyDrawer(typeof(FlowEventsBuilder))]
    public class FlowEventsBuilderPropertyDrawer : AbstractEventsBuilderPropertyDrawer<FlowEventsBuilderPropertyDrawer.FieldsEnum>
    {
        public enum FieldsEnum
        {
            onStarted,
            onUpdated,
            onLoopCompleted,
            onCompleted,
        }
    }
}
